using FluffySpoon.Automation.Web.Dom;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.Events;
using System;
using System.Collections.Generic;
using System.Linq;
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
			IDomSelectorStrategy domSelectorStrategy,
			IDomElementFactory domElementFactory,
			IWebDriver driver)
		{
			_driver = new EventFiringWebDriver(driver);
			_semaphore = new SemaphoreSlim(1);

			_domElementFactory = domElementFactory;
			_domSelectorStrategy = domSelectorStrategy;

			_uniqueSelectorAttribute = "fluffyspoon-tag-" + Guid.NewGuid();
		}

		public void Dispose()
		{
			_driver.Quit();
			_driver.Dispose();
		}
		
		public Task EnterTextInAsync(IReadOnlyList<IDomElement> elements, string text)
		{
			var nativeElements = GetWebDriverElementsFromDomElements(elements);
			foreach (var nativeElement in nativeElements)
			{
				nativeElement.Clear();
				nativeElement.SendKeys(text);
			}

			return Task.CompletedTask;
		}

		public async Task<IReadOnlyList<IDomElement>> EvaluateJavaScriptAsDomElementsAsync(string code)
		{
			var scriptExecutor = GetScriptExecutor();
			
			var prefix = Guid.NewGuid().ToString();
			var tags = (IReadOnlyList<object>)scriptExecutor.ExecuteScript(@"
				return (function() { 
					var elements=(function() {" + code + @"})(); 
					var tags=[];
					for(var i=0;i<elements.length;i++) {
						tags.push(elements[i].getAttribute('" + _uniqueSelectorAttribute + @"')||'" + prefix + @"-'+i);
						elements[i].setAttribute('" + _uniqueSelectorAttribute + @"',tags[tags.length-1]);
					}
					return tags;
	 			})()");

			return tags
				.Select(x => _domElementFactory.Create("[" + _uniqueSelectorAttribute + "='" + x.ToString() + "']"))
				.ToArray();
		}

		public Task<string> EvaluateJavaScriptAsync(string code)
		{
			var scriptExecutor = GetScriptExecutor();
			var result = scriptExecutor.ExecuteScript(code);

			return Task.FromResult(result?.ToString());
		}

		public async Task<IReadOnlyList<IDomElement>> FindDomElementsAsync(string selector)
		{
			return await EvaluateJavaScriptAsDomElementsAsync(
				_domSelectorStrategy.GetJavaScriptForRetrievingDomElements(selector));
		}

		public async Task OpenAsync(string uri)
		{
			await _semaphore.WaitAsync();

			var navigatedWaitHandle = new SemaphoreSlim(0);

			void DriverNavigated(object sender, WebDriverNavigationEventArgs e)
			{
				if (e.Url != uri) return;
				
				_driver.Navigated -= DriverNavigated;
				EvaluateJavaScriptAsync(_domSelectorStrategy.InitialJavaScriptForEachPage)
					.ContinueWith(_ => navigatedWaitHandle.Release(1));
			}

			_driver.Navigated += DriverNavigated;
			_driver.Navigate().GoToUrl(uri);

			await navigatedWaitHandle.WaitAsync();
			_semaphore.Release();
		}

		private IJavaScriptExecutor GetScriptExecutor()
		{
			if (!(_driver is IJavaScriptExecutor scriptExecutor))
				throw new InvalidOperationException("The given Selenium web driver does not support JavaScript execution.");

			return scriptExecutor;
		}

		private IWebElement[] GetWebDriverElementsFromDomElements(IReadOnlyList<IDomElement> domElements)
		{
			var selector = domElements
				.Select(x => x.CssSelector)
				.Aggregate((a, b) => $"{a}, {b}");
			return _driver
				.FindElements(By.CssSelector(selector))
				.ToArray();
		}
	}
}
