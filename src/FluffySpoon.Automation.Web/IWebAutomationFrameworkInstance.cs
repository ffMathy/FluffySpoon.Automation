using FluffySpoon.Automation.Web.Dom;
using SkiaSharp;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FluffySpoon.Automation.Web
{
	public interface IWebAutomationFrameworkInstance : IDisposable
	{
		bool IsNavigating { get; }
		string UserAgentName { get; }

		Task<IReadOnlyList<IDomElement>> FindDomElementsBySelectorAsync(int methodChainOffset, string selector);
		Task<IReadOnlyList<IDomElement>> FindDomElementsByCssSelectorsAsync(int methodChainOffset, string[] selectors);
		Task<string> EvaluateJavaScriptAsync(string code);

		Task OpenAsync(string uri);
		Task DragDropAsync(IDomElement from, int fromOffsetX, int fromOffsetY, IDomElement to, int toOffsetX, int toOffsetY);
		Task FocusAsync(IDomElement domElement);
		Task EnterTextInAsync(IReadOnlyList<IDomElement> elements, string text);
		Task HoverAsync(IDomElement domElement, int offsetX, int offsetY);
		Task ClickAsync(IReadOnlyList<IDomElement> elements, int offsetX, int offsetY);
		Task SelectByIndicesAsync(IReadOnlyList<IDomElement> elements, int[] byIndices);
		Task SelectByTextsAsync(IReadOnlyList<IDomElement> elements, string[] byTexts);
		Task SelectByValuesAsync(IReadOnlyList<IDomElement> elements, string[] byValues);
		Task RightClickAsync(IReadOnlyList<IDomElement> elements, int offsetX, int offsetY);
		Task DoubleClickAsync(IReadOnlyList<IDomElement> elements, int offsetX, int offsetY);

		Task<SKBitmap> TakeScreenshotAsync();
		Task InitializeAsync();
	}
}