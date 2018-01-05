using FluffySpoon.Automation.Web.Dom;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace FluffySpoon.Automation.Web.Selenium
{
	class SeleniumWebAutomationFrameworkInstance : IWebAutomationFrameworkInstance
	{
		private readonly EventFiringWebDriver _driver;
		private readonly SemaphoreSlim _semaphore;

		private readonly IDomElementFactory _domElementFactory;
		private readonly IDomSelectorStrategy _domSelectorStrategy;

		private readonly string _uniqueSelectorAttribute;

		public SeleniumWebAutomationFrameworkInstance(
			IDomSelectorStrategyFactory domSelectorStrategyFactory,
			IDomElementFactory domElementFactory,
			IWebDriver driver)
		{
			_driver = new EventFiringWebDriver(driver);
			_semaphore = new SemaphoreSlim(1);

			_domElementFactory = domElementFactory;
			_domSelectorStrategy = domSelectorStrategyFactory.Create(this);

			_uniqueSelectorAttribute = "fluffyspoon-tag-" + Guid.NewGuid();
		}

		public void Dispose()
		{
			_driver.Quit();
			_driver.Dispose();
		}

		public async Task EnterTextInAsync(string text, string selector)
		{
			var elements = await GetElementsFromSelectorAsync(selector);
			foreach (var element in elements)
			{
				element.Clear();
				element.SendKeys(text);
			}
		}

		public async Task<IReadOnlyList<IDomElement>> EvaluateJavaScriptAsDomElementsAsync(string code)
		{
			var domElements = new List<IDomElement>();

			var scriptExecutor = GetScriptExecutor();

			var seleniumElements = (IReadOnlyList<IWebElement>)scriptExecutor.ExecuteScript(code);
			foreach (var seleniumElement in seleniumElements) {
				var elementTagString = seleniumElement.GetAttribute(_uniqueSelectorAttribute);
				if (elementTagString == null)
				{
					elementTagString = Guid.NewGuid().ToString();
					scriptExecutor.ExecuteScript(
						"arguments[0].setAttribute(arguments[1], arguments[2]);",
						seleniumElement,
						_uniqueSelectorAttribute,
						elementTagString);
				}

				var inlinedElementTagString = PrepareCssSelectorForInlining(elementTagString);
				domElements.Add(_domElementFactory.Create(
					"[" + _uniqueSelectorAttribute + "='" + inlinedElementTagString + "']"));
			}

			return domElements;
		}

		public Task<string> EvaluateJavaScriptAsync(string code)
		{
			var scriptExecutor = GetScriptExecutor();
			var result = scriptExecutor.ExecuteScript(code);

			return Task.FromResult(result?.ToString());
		}

		public async Task OpenAsync(string uri)
		{
			await _semaphore.WaitAsync();

			var navigatedWaitHandle = new SemaphoreSlim(0);

			void DriverNavigated(object sender, WebDriverNavigationEventArgs e) {
				_driver.Navigated -= DriverNavigated;
				if (e.Url == uri)
					navigatedWaitHandle.Release();
			}

			_driver.Navigated += DriverNavigated;
			_driver.Navigate().GoToUrl(uri);

			await navigatedWaitHandle.WaitAsync();
			_semaphore.Release();
		}

		private IJavaScriptExecutor GetScriptExecutor()
		{
			var scriptExecutor = _driver as IJavaScriptExecutor;
			if (scriptExecutor == null)
				throw new InvalidOperationException("The given Selenium web driver does not support JavaScript execution.");

			return scriptExecutor;
		}

		private static string PrepareCssSelectorForInlining(string selector)
		{
			return selector.Replace("'", "\\'");
		}

		private async Task<IReadOnlyList<IWebElement>> GetElementsFromSelectorAsync(string selector)
		{
			var domElements = await _domSelectorStrategy.GetDomElementsAsync(selector);
			return domElements
				.Select(e => _driver.FindElement(By.CssSelector(e.CssSelector)))
				.ToArray();
		}
	}
}
