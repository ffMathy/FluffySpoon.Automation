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
		private readonly EventFiringWebDriver _driver;
		private readonly SemaphoreSlim _semaphore;

		private readonly IDomSelectorStrategy _domSelectorStrategy;

		private readonly static string _uniqueSelectorAttribute;

		private Actions Actions => new Actions(_driver);

		public string UserAgentName { get; }

		static SeleniumWebAutomationFrameworkInstance()
		{
			_uniqueSelectorAttribute = "fluffyspoon-tag-" + Guid.NewGuid();
		}

		public SeleniumWebAutomationFrameworkInstance(
			IDomSelectorStrategy domSelectorStrategy,
			IWebDriver driver)
		{
			UserAgentName = driver.GetType().Name;

			_driver = new EventFiringWebDriver(driver);
			_semaphore = new SemaphoreSlim(1);

			_domSelectorStrategy = domSelectorStrategy;
		}

		public Task FocusAsync(IDomElement domElement, int offsetX, int offsetY)
		{
			var nativeElement = GetWebDriverElementsFromDomElements(new[] { domElement }).Single();
			GetScriptExecutor().ExecuteScript("arguments[0].focus();", nativeElement);

			return Task.CompletedTask;
		}

		public async Task DragDropAsync(IDomElement from, int fromOffsetX, int fromOffsetY, IDomElement to, int toOffsetX, int toOffsetY)
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
		}

		public async Task HoverAsync(IDomElement element, int relativeX, int relativeY)
		{
			await PerformMouseOperationOnElementCoordinatesAsync((a, b) => a, new[] { element }, relativeX, relativeY);
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

		public void Dispose()
		{
			_driver.Quit();
			_driver.Dispose();
		}

		public async Task EnterTextInAsync(IReadOnlyList<IDomElement> elements, string text)
		{
			var nativeElements = GetWebDriverElementsFromDomElements(elements);
			foreach (var nativeElement in nativeElements)
			{
				nativeElement.Clear();
				nativeElement.SendKeys(text);
			}
		}

		public async Task<IReadOnlyList<IDomElement>> EvaluateJavaScriptAsDomElementsAsync(
			int methodChainOffset,
			string javascriptCode)
		{
			var scriptExecutor = GetScriptExecutor();
			
			var elementFetchJavaScript = WrapJavaScriptInIsolatedFunction(
				_domSelectorStrategy.DomSelectorLibraryJavaScript + "; " + javascriptCode);

			var resultJsonBlobs = (IReadOnlyList<object>)scriptExecutor.ExecuteScript(@"
				return " + WrapJavaScriptInIsolatedFunction(@"
					var elements = " + elementFetchJavaScript + @";
					var returnValues = [];

					for(var i = 0; i < elements.length; i++) {
						var element = elements[i];

						var attributes = [];
						var computedStyleProperties = [];
						
						var tag = element.getAttribute('" + _uniqueSelectorAttribute + @"') || '" + methodChainOffset + @"-'+i;
						element.setAttribute('" + _uniqueSelectorAttribute + @"', tag);
						
						var o;

						for(o = 0; o < element.attributes.length; o++) {
							var attribute = element.attributes[o];
							attributes.push({
								name: attribute.name,
								value: attribute.value
							});
						}

						var computedStyle = getComputedStyle(element);
						for(o = 0; o < computedStyle.length; o++) {
							var styleKey = computedStyle[o];
							computedStyleProperties.push({
								property: styleKey,
								value: computedStyle.getPropertyValue(styleKey)
							});
						}

						var boundingClientRectangle = element.getBoundingClientRect();

						returnValues.push(JSON.stringify({
							tag: tag,
							attributes: attributes,
							computedStyle: computedStyleProperties,
							textContent: element.textContent,
							value: element.value,
							clientLeft: element.clientLeft,
							clientTop: element.clientTop,
							clientWidth: element.clientWidth,
							clientHeight: element.clientHeight,
							boundingClientRectangle: {
								left: boundingClientRectangle.left,
								right: boundingClientRectangle.right,
								top: boundingClientRectangle.top,
								bottom: boundingClientRectangle.bottom
							}
						}));
					}

					return returnValues;
				"));

			return resultJsonBlobs
				.Cast<string>()
				.Select(JsonConvert.DeserializeObject<ElementWrapper>)
				.Select(x =>
				{
					var attributes = new DomAttributes();
					foreach (var attribute in x.Attributes)
						attributes.Add(attribute.Name, attribute.Value);

					var computedStyle = new DomStyle(x.ComputedStyle);

					var domElement = new DomElement(
						cssSelector: "[" + _uniqueSelectorAttribute + "='" + x.Tag + "']",
						textContent: x.TextContent,
						value: x.Value,
						clientLeft: x.ClientLeft,
						clientTop: x.ClientTop,
						clientWidth: x.ClientWidth,
						clientHeight: x.ClientHeight,
						boundingClientRectangle: x.BoundingClientRectangle,
						attributes: attributes,
						computedStyle: computedStyle);
					Console.WriteLine(UserAgentName + " found DOM element " + domElement.CssSelector);

					return domElement;
				})
				.ToArray();
		}

		public Task<string> EvaluateJavaScriptAsync(string code)
		{
			var scriptExecutor = GetScriptExecutor();
			var result = scriptExecutor.ExecuteScript(code);

			return Task.FromResult(result?.ToString());
		}

		public async Task<IReadOnlyList<IDomElement>> FindDomElementsBySelectorAsync(
			int methodChainOffset, 
			string selector)
		{
			Console.WriteLine(UserAgentName + " finding DOM elements by selector " + selector);

			var scriptToExecute = _domSelectorStrategy.GetJavaScriptForRetrievingDomElements(selector);
			return await EvaluateJavaScriptAsDomElementsAsync(methodChainOffset, scriptToExecute);
		}

		public async Task OpenAsync(string uri)
		{
			await _semaphore.WaitAsync();

			var navigatedWaitHandle = new SemaphoreSlim(0);

			async void DriverNavigated(object sender, WebDriverNavigationEventArgs e)
			{
				if (e.Url != uri) return;

				_driver.Navigated -= DriverNavigated;

				while (GetReadyState() != "complete")
					await Task.Delay(10);

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

		private string WrapJavaScriptInIsolatedFunction(string code)
		{
			return $"(function() {{{code}}})();";
		}

		private IWebElement[] GetWebDriverElementsFromDomElements(IReadOnlyList<IDomElement> domElements)
		{
			var selector = domElements
				.Select(x => x.CssSelector)
				.Aggregate((a, b) => $"{a}, {b}");
		
			Console.WriteLine(UserAgentName + " finding driver elements by selector " + selector);
			
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
			var combinedSelector = selectors.Aggregate((a, b) => a + ", " + b);
			var sanitizedSelector = combinedSelector.Replace("'", "\\'");
			return await EvaluateJavaScriptAsDomElementsAsync(
				methodChainOffset, 
				@"return document.querySelectorAll('" + sanitizedSelector + "')");
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

		private class ElementWrapper
		{
			public string Tag { get; set; }
			public string TextContent { get; set; }
			public string Value { get; set; }

			public int ClientLeft { get; set; }
			public int ClientTop { get; set; }
			public int ClientWidth { get; set; }
			public int ClientHeight { get; set; }

			public DomRectangle BoundingClientRectangle { get; set; }

			public DomAttribute[] Attributes { get; set; }
			public DomStyleProperty[] ComputedStyle { get; set; }
		}
	}
}
