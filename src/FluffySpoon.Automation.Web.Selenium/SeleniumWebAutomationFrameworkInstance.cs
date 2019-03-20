using FluffySpoon.Automation.Web.Dom;
using Newtonsoft.Json;
using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Support.Events;
using OpenQA.Selenium.Support.UI;
using SkiaSharp;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace FluffySpoon.Automation.Web.Selenium
{
	class SeleniumWebAutomationFrameworkInstance : IWebAutomationFrameworkInstance
	{
		private EventFiringWebDriver _driver;

		private readonly SemaphoreSlim _semaphore;

		private readonly Func<Task<IWebDriver>> _driverConstructor;

		private readonly IJavaScriptTunnel _domTunnel;

		private Actions Actions => new Actions(_driver);

		public string UserAgentName { get; private set; }

		public bool IsNavigating { get; private set; }

		public SeleniumWebAutomationFrameworkInstance(
			Func<Task<IWebDriver>> driverConstructor,
			IJavaScriptTunnel domTunnel)
		{
			_driverConstructor = driverConstructor;
			_domTunnel = domTunnel;

			_semaphore = new SemaphoreSlim(1);
		}

		public Task FocusAsync(IDomElement domElement)
		{
			var nativeElement = GetWebDriverElementsFromDomElements(new[] { domElement }).Single();
			GetScriptExecutor().ExecuteScript("arguments[0].focus();", nativeElement);

			return Task.CompletedTask;
		}

		public Task DragDropAsync(IDomElement from, int fromOffsetX, int fromOffsetY, IDomElement to, int toOffsetX, int toOffsetY)
		{
			var nativeElements = GetWebDriverElementsFromDomElements(new[] { from, to });
			var nativeFromElement = nativeElements[0];
			var nativeToElement = nativeElements[1];

            Actions
                .MoveToElement(
                    nativeFromElement,
                    fromOffsetX,
                    fromOffsetY)
                .ClickAndHold()
                .MoveToElement(
                    nativeToElement,
                    toOffsetX,
                    toOffsetY)
                .Release()
                .Build()
                .Perform();

            return Task.CompletedTask;
		}

		public async Task HoverAsync(IDomElement element, int relativeX, int relativeY)
		{
			await PerformMouseOperationOnElementCoordinatesAsync((a, b) => a.MoveToElement(b), new[] { element }, relativeX, relativeY);
		}

		public async Task ClickAsync(IReadOnlyList<IDomElement> elements, int relativeX, int relativeY)
		{
			await PerformMouseOperationOnElementCoordinatesAsync((a, b) => a.Click(b), elements, relativeX, relativeY);
		}

		public async Task DoubleClickAsync(IReadOnlyList<IDomElement> elements, int relativeX, int relativeY)
		{
			await PerformMouseOperationOnElementCoordinatesAsync((a, b) => a.DoubleClick(b), elements, relativeX, relativeY);
		}

		public async Task RightClickAsync(IReadOnlyList<IDomElement> elements, int relativeX, int relativeY)
		{
			await PerformMouseOperationOnElementCoordinatesAsync((a, b) => a.ContextClick(b), elements, relativeX, relativeY);
		}

		public Task DisposeAsync()
		{
			_driver?.Quit();
			_driver?.Dispose();

			return Task.CompletedTask;
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

		public Task<string> EvaluateJavaScriptAsync(string code)
		{
			var scriptExecutor = GetScriptExecutor();
			var result = scriptExecutor.ExecuteScript("return " + code);

			return Task.FromResult(result?.ToString());
		}

		public async Task<IReadOnlyList<IDomElement>> FindDomElementsBySelectorAsync(
			int methodChainOffset,
			string selector)
		{
			return await _domTunnel.GetDomElementsFromSelector(this,
				methodChainOffset,
				selector);
		}

		public async Task OpenAsync(string uri)
		{
			await _semaphore.WaitAsync();

			var navigatedWaitHandle = new SemaphoreSlim(0);

			async void DriverNavigated(object sender, WebDriverNavigationEventArgs e)
			{
				if (e.Url != uri)
					return;

				_driver.Navigated -= DriverNavigated;

				while (GetReadyState() != "complete")
					await Task.Delay(100);

				navigatedWaitHandle.Release(1);
			}

			_driver.Navigated += DriverNavigated;
			_driver.Navigate().GoToUrl(uri);

			await navigatedWaitHandle.WaitAsync();
			_semaphore.Release();
		}

		private string GetReadyState()
		{
			return _driver.ExecuteScript("return document.readyState")?.ToString();
		}

		private ITakesScreenshot GetScreenshotDriver()
		{
			if (!(_driver is ITakesScreenshot screenshotDriver))
				throw new InvalidOperationException("The given Selenium web driver does not support taking screenshots.");

			return screenshotDriver;
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

		public async Task<SKBitmap> TakeScreenshotAsync()
		{
			var currentDriverDimensions = _driver.Manage().Window.Size;

			try
			{
				var bodyDimensionsBlob = await EvaluateJavaScriptAsync(@"
					return JSON.stringify({
						document: {
							width: Math.max(
								document.body.scrollWidth, 
								document.body.offsetWidth, 
								document.documentElement.clientWidth, 
								document.documentElement.scrollWidth, 
								document.documentElement.offsetWidth),
							height: Math.max(
								document.body.scrollHeight, 
								document.body.offsetHeight, 
								document.documentElement.clientHeight, 
								document.documentElement.scrollHeight, 
								document.documentElement.offsetHeight)
						},
						window: {
							width: window.outerWidth,
							height: window.outerHeight
						}
					});
				");
				var bodyDimensions = JsonConvert.DeserializeObject<GlobalDimensionsWrapper>(bodyDimensionsBlob);

				var newDriverDimensions = new Size()
				{
					Width = bodyDimensions.Document.Width + (currentDriverDimensions.Width - bodyDimensions.Window.Width),
					Height = bodyDimensions.Document.Height + (currentDriverDimensions.Height - bodyDimensions.Window.Height)
				};

				_driver.Manage().Window.Size = newDriverDimensions;

				var screenshot = GetScreenshotDriver().GetScreenshot();
				return SKBitmap.Decode(screenshot.AsByteArray);
			}
			finally
			{
				_driver.Manage().Window.Size = currentDriverDimensions;
			}
		}

		private async Task PerformMouseOperationOnElementCoordinatesAsync(
			Func<Actions, IWebElement, Actions> operation,
			IReadOnlyList<IDomElement> elements,
			int relativeX,
			int relativeY)
		{
			var nativeElements = GetWebDriverElementsFromDomElements(elements);
			foreach (var nativeElement in nativeElements)
			{
				operation(Actions.MoveToElement(nativeElement, relativeX, relativeY), nativeElement)
					.Build()
					.Perform();
			}
		}

		private void SelectAsync(IReadOnlyList<IDomElement> elements, Action<SelectElement> action)
		{
			var nativeElements = GetWebDriverElementsFromDomElements(elements);
			foreach (var nativeElement in nativeElements)
			{
				var selectElement = new SelectElement(nativeElement);
				if (selectElement.IsMultiple)
					selectElement.DeselectAll();

				action(selectElement);
			}
		}

		public Task SelectByIndicesAsync(IReadOnlyList<IDomElement> elements, int[] indices)
		{
			SelectAsync(elements, selectElement =>
			{
				foreach (var index in indices)
				{
					selectElement.SelectByIndex(index);
				}
			});
			return Task.CompletedTask;
		}

		public Task SelectByTextsAsync(IReadOnlyList<IDomElement> elements, string[] texts)
		{
			SelectAsync(elements, selectElement =>
			{
				foreach (var text in texts)
				{
					selectElement.SelectByText(text);
				}
			});
			return Task.CompletedTask;
		}

		public Task SelectByValuesAsync(IReadOnlyList<IDomElement> elements, string[] values)
		{
			SelectAsync(elements, selectElement =>
			{
				foreach (var value in values)
				{
					selectElement.SelectByValue(value);
				}
			});
			return Task.CompletedTask;
		}

		public async Task<IReadOnlyList<IDomElement>> FindDomElementsByCssSelectorsAsync(
			int methodChainOffset,
			string[] selectors)
		{
			return await _domTunnel.FindDomElementsByCssSelectorsAsync(this,
				methodChainOffset,
				selectors);
		}

		public async Task InitializeAsync()
		{
			var driver = await _driverConstructor();
			UserAgentName = driver.GetType().Name;

			_driver = new EventFiringWebDriver(driver);
			_driver.Navigating += DriverNavigating;
			_driver.Navigated += DriverNavigated;
		}

		private void DriverNavigated(object sender, WebDriverNavigationEventArgs e)
		{
			IsNavigating = false;
		}

		private void DriverNavigating(object sender, WebDriverNavigationEventArgs e)
		{
			IsNavigating = true;
		}

		private class DimensionsWrapper
		{
			public int Width { get; set; }
			public int Height { get; set; }
		}

		private class GlobalDimensionsWrapper
		{
			public DimensionsWrapper Window { get; set; }
			public DimensionsWrapper Document { get; set; }
		}
	}
}
