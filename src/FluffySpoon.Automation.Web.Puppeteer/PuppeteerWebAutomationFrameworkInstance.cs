using FluffySpoon.Automation.Web.Dom;
using PuppeteerSharp;
using PuppeteerSharp.Input;
using SkiaSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FluffySpoon.Automation.Web.Puppeteer
{
	class PuppeteerWebAutomationFrameworkInstance : IWebAutomationFrameworkInstance
	{
		private Browser _browser;
		private Page _page;

		private readonly Func<Task<Browser>> _driverConstructor;
		private readonly IDomTunnel _domTunnel;

		public PuppeteerWebAutomationFrameworkInstance(
			Func<Task<Browser>> driverConstructor,
			IDomTunnel domTunnel)
		{
			_driverConstructor = driverConstructor;
			_domTunnel = domTunnel;
		}

		public string UserAgentName => GetType().Name;

		private async Task<ElementHandle[]> GetElementHandlesFromDomElementsAsync(IReadOnlyList<IDomElement> domElements)
		{
			var selector = domElements
				.Select(x => x.CssSelector)
				.Aggregate((a, b) => $"{a}, {b}");

			return await _page.QuerySelectorAllAsync(selector);
		}

		public async Task ClickAsync(IReadOnlyList<IDomElement> elements, int offsetX, int offsetY)
		{
			foreach (var element in elements)
			{
				await _page.Mouse.ClickAsync(
					(int)Math.Ceiling(element.BoundingClientRectangle.Left) + offsetX,
					(int)Math.Ceiling(element.BoundingClientRectangle.Top) + offsetY);
			}
		}

		public void Dispose()
		{
			_page?.Dispose();
			_browser?.Dispose();
		}

		public async Task DoubleClickAsync(IReadOnlyList<IDomElement> elements, int offsetX, int offsetY)
		{
			foreach (var element in elements)
			{
				await _page.Mouse.ClickAsync(
					(int)Math.Ceiling(element.BoundingClientRectangle.Left) + offsetX,
					(int)Math.Ceiling(element.BoundingClientRectangle.Top) + offsetY,
					new ClickOptions() {
						ClickCount = 2
					});
			}
		}

		public async Task DragDropAsync(IDomElement from, int fromOffsetX, int fromOffsetY, IDomElement to, int toOffsetX, int toOffsetY)
		{
			await _page.Mouse.MoveAsync(
				(int)Math.Ceiling(from.BoundingClientRectangle.Left) + fromOffsetX,
				(int)Math.Ceiling(from.BoundingClientRectangle.Top) + fromOffsetY);
			await _page.Mouse.DownAsync();

			await _page.Mouse.MoveAsync(
				(int)Math.Ceiling(to.BoundingClientRectangle.Left) + toOffsetX,
				(int)Math.Ceiling(to.BoundingClientRectangle.Top) + toOffsetY);
			await _page.Mouse.UpAsync();
		}

		public async Task EnterTextInAsync(IReadOnlyList<IDomElement> elements, string text)
		{
			var handles = await GetElementHandlesFromDomElementsAsync(elements);
			foreach(var handle in handles) { 
				await handle.TypeAsync(text);
			}
		}

		public async Task<string> EvaluateJavaScriptAsync(string code)
		{
			var blob = await _page.EvaluateExpressionAsync(code);
			return blob.ToString();
		}

		public Task<IReadOnlyList<IDomElement>> FindDomElementsBySelectorAsync(int methodChainOffset, string selector)
		{
			return _domTunnel.GetDomElementsFromSelector(this,
				methodChainOffset,
				selector);
		}

		public async Task FocusAsync(IDomElement domElement)
		{
			var handle = await GetElementHandleFromDomElementAsync(domElement);
			await handle.FocusAsync();
		}

		private async Task<ElementHandle> GetElementHandleFromDomElementAsync(IDomElement domElement)
		{
			return await _page.QuerySelectorAsync(domElement.CssSelector);
		}

		public async Task HoverAsync(IDomElement domElement, int offsetX, int offsetY)
		{
			var handle = await GetElementHandleFromDomElementAsync(domElement);
			await _page.Mouse.MoveAsync(
				(int)Math.Ceiling(domElement.BoundingClientRectangle.Left) + offsetX,
				(int)Math.Ceiling(domElement.BoundingClientRectangle.Top) + offsetY);
		}

		public async Task InitializeAsync()
		{
			_browser = await _driverConstructor();

			var pages = await _browser.PagesAsync();
			_page = pages.Single();
		}

		public async Task OpenAsync(string uri)
		{
			await _page.GoToAsync(uri);
			await _page.WaitForExpressionAsync("document.readyState === 'complete'");
		}

		public async Task RightClickAsync(IReadOnlyList<IDomElement> elements, int offsetX, int offsetY)
		{
			foreach (var element in elements)
			{
				await _page.Mouse.ClickAsync(
					(int)Math.Ceiling(element.BoundingClientRectangle.Left) + offsetX,
					(int)Math.Ceiling(element.BoundingClientRectangle.Top) + offsetY,
					new ClickOptions() {
						Button = MouseButton.Right
					});
			}
		}

		public async Task SelectByIndicesAsync(IReadOnlyList<IDomElement> elements, int[] byIndices)
		{
			foreach (var element in elements)
			{
				var selector = byIndices
					.Select(x => $"{element.CssSelector} > option:nth-child({x+1})")
					.Aggregate(string.Empty, (a, b) => $"{a}, {b}");
				var handles = await _page.QuerySelectorAllAsync(selector);
				var valueTasks = handles.Select(x => _page.EvaluateFunctionAsync("x => x.value", x));
				var valueTokens = await Task.WhenAll(valueTasks);
				var values = valueTokens.Cast<string>();
				await _page.SelectAsync(element.CssSelector, values.ToArray());
			}
		}

		public async Task SelectByTextsAsync(IReadOnlyList<IDomElement> elements, string[] byTexts)
		{
			var trimmedByTexts = byTexts
				.Select(x => x.Trim())
				.ToArray();
			foreach (var element in elements)
			{
				var selector = byTexts
					.Select(x => $"{element.CssSelector} > option")
					.Aggregate(string.Empty, (a, b) => $"{a}, {b}");
				var handles = await _page.QuerySelectorAllAsync(selector);
				var tasks = handles.Select(x => _page.EvaluateFunctionAsync("x => { return { value: x.value, textContent: x.textContent } }", x));
				var tokens = await Task.WhenAll(tasks);
				var values = tokens
					.Select(x => new {
						Value = x.Value<string>("value"),
						TextContent = x.Value<string>("textContent")
					})
					.Where(x => trimmedByTexts.Contains(x.TextContent?.Trim()))
					.Select(x => x.Value);
				await _page.SelectAsync(element.CssSelector, values.ToArray());
			}
		}

		public async Task SelectByValuesAsync(IReadOnlyList<IDomElement> elements, string[] byValues)
		{
			var selector = elements
				.Select(x => x.CssSelector)
				.Aggregate(string.Empty, (a, b) => $"{a}, {b}");
			await _page.SelectAsync(selector, byValues);
		}

		public async Task<SKBitmap> TakeScreenshotAsync()
		{
			var bytes = await _page.ScreenshotDataAsync();
			return SKBitmap.Decode(bytes);
		}

		public async Task<IReadOnlyList<IDomElement>> FindDomElementsByCssSelectorsAsync(int methodChainOffset, string[] selectors)
		{
			return await FindDomElementsBySelectorAsync(
				methodChainOffset,
				selectors.Aggregate(string.Empty, (a, b) => $"{a}, {b}"));
		}
	}
}
