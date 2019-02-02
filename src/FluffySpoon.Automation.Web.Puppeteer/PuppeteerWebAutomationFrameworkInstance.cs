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
					element.ClientLeft + offsetX,
					element.ClientTop + offsetY);
			}
		}

		public void Dispose()
		{
			_page.Dispose();
			_browser.Dispose();
		}

		public async Task DoubleClickAsync(IReadOnlyList<IDomElement> elements, int offsetX, int offsetY)
		{
			foreach (var element in elements)
			{
				await _page.Mouse.ClickAsync(
					element.ClientLeft + offsetX,
					element.ClientTop + offsetY,
					new ClickOptions() {
						ClickCount = 2
					});
			}
		}

		public async Task DragDropAsync(IDomElement from, int fromOffsetX, int fromOffsetY, IDomElement to, int toOffsetX, int toOffsetY)
		{
			await _page.Mouse.MoveAsync(
				from.ClientLeft + fromOffsetX,
				from.ClientTop + fromOffsetY);
			await _page.Mouse.DownAsync();

			await _page.Mouse.MoveAsync(
				to.ClientLeft + toOffsetX,
				to.ClientTop + toOffsetY);
			await _page.Mouse.UpAsync();
		}

		public async Task EnterTextInAsync(IReadOnlyList<IDomElement> elements, string text)
		{
			var handles = await GetElementHandlesFromDomElementsAsync(elements);
			foreach(var handle in handles)
				await handle.TypeAsync(text);
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
				domElement.ClientLeft + offsetX,
				domElement.ClientTop + offsetY);
		}

		public async Task InitializeAsync()
		{
			_browser = await _driverConstructor();
			_page = await _browser.NewPageAsync();
		}

		public Task OpenAsync(string uri)
		{
			return _page.GoToAsync(uri);
		}

		public async Task RightClickAsync(IReadOnlyList<IDomElement> elements, int offsetX, int offsetY)
		{
			foreach (var element in elements)
			{
				await _page.Mouse.ClickAsync(
					element.ClientLeft + offsetX,
					element.ClientTop + offsetY,
					new ClickOptions() {
						Button = MouseButton.Right
					});
			}
		}

		public async Task SelectByIndicesAsync(IReadOnlyList<IDomElement> elements, int[] byIndices)
		{
			foreach (var element in elements)
			{
				var handle = await _page.QuerySelectorAsync(element.CssSelector);
				await _page.SelectAsync(handle)
			}
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

		public Task<IReadOnlyList<IDomElement>> FindDomElementsByCssSelectorsAsync(int methodChainOffset, string[] selectors)
		{
			throw new NotImplementedException();
		}
	}
}
