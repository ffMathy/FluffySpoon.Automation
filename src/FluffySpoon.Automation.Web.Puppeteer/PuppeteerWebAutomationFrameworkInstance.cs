using FluffySpoon.Automation.Web.Dom;
using Newtonsoft.Json;
using PuppeteerSharp;
using PuppeteerSharp.Input;
using SkiaSharp;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace FluffySpoon.Automation.Web.Selenium
{
	class PuppeteerWebAutomationFrameworkInstance : IWebAutomationFrameworkInstance
	{
		private Browser _browser;
		private Page _page;

		public string UserAgentName => GetType().Name;
		
		private string GetSelectorForDomElements(IReadOnlyList<IDomElement> domElements)
		{
			var selector = domElements
				.Select(x => x.CssSelector)
				.Aggregate((a, b) => $"{a}, {b}");

			return selector;
		}

		public Task ClickAsync(IReadOnlyList<IDomElement> elements, int offsetX, int offsetY)
		{
			
			return _page.ClickAsync(
				GetSelectorForDomElements(elements),
				new ClickOptions() {
					
				});
		}

		public void Dispose()
		{
			throw new NotImplementedException();
		}

		public Task DoubleClickAsync(IReadOnlyList<IDomElement> elements, int offsetX, int offsetY)
		{
			throw new NotImplementedException();
		}

		public Task DragDropAsync(IDomElement from, int fromOffsetX, int fromOffsetY, IDomElement to, int toOffsetX, int toOffsetY)
		{
			throw new NotImplementedException();
		}

		public Task EnterTextInAsync(IReadOnlyList<IDomElement> elements, string text)
		{
			throw new NotImplementedException();
		}

		public Task<IReadOnlyList<IDomElement>> EvaluateJavaScriptAsDomElementsAsync(int methodChainOffset, string code)
		{
			throw new NotImplementedException();
		}

		public Task<string> EvaluateJavaScriptAsync(string code)
		{
			throw new NotImplementedException();
		}

		public Task<IReadOnlyList<IDomElement>> FindDomElementsByCssSelectorsAsync(int methodChainOffset, string[] selectors)
		{
			throw new NotImplementedException();
		}

		public Task<IReadOnlyList<IDomElement>> FindDomElementsBySelectorAsync(int methodChainOffset, string selector)
		{
			throw new NotImplementedException();
		}

		public Task FocusAsync(IDomElement domElement, int offsetX, int offsetY)
		{
			throw new NotImplementedException();
		}

		public Task HoverAsync(IDomElement domElement, int offsetX, int offsetY)
		{
			throw new NotImplementedException();
		}

		public async Task InitializeAsync()
		{
			await new BrowserFetcher().DownloadAsync(BrowserFetcher.DefaultRevision); 
			
			_browser = await Puppeteer.LaunchAsync(new LaunchOptions
			{
				Headless = true
			});
			_page = await _browser.NewPageAsync();
		}

		public Task OpenAsync(string uri)
		{
			throw new NotImplementedException();
		}

		public Task RightClickAsync(IReadOnlyList<IDomElement> elements, int offsetX, int offsetY)
		{
			throw new NotImplementedException();
		}

		public Task SelectByIndicesAsync(IReadOnlyList<IDomElement> elements, int[] byIndices)
		{
			throw new NotImplementedException();
		}

		public Task SelectByTextsAsync(IReadOnlyList<IDomElement> elements, string[] byTexts)
		{
			throw new NotImplementedException();
		}

		public Task SelectByValuesAsync(IReadOnlyList<IDomElement> elements, string[] byValues)
		{
			throw new NotImplementedException();
		}

		public Task<SKBitmap> TakeScreenshotAsync()
		{
			throw new NotImplementedException();
		}
	}
}
