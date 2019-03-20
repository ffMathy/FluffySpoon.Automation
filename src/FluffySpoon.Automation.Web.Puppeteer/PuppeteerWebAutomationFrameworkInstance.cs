using FluffySpoon.Automation.Web.Dom;
using PuppeteerSharp;
using PuppeteerSharp.Input;
using SkiaSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;
using System.Threading;
using FluffySpoon.Automation.Web.Exceptions;

namespace FluffySpoon.Automation.Web.Puppeteer
{
    class PuppeteerWebAutomationFrameworkInstance : IWebAutomationFrameworkInstance
    {
        private Browser _browser;
        private Page _page;

        private int _pendingNavigationRequestCount;

        private readonly Func<Task<Browser>> _driverConstructor;
        private readonly IJavaScriptTunnel _domTunnel;

        private bool _isDisposed;

        public PuppeteerWebAutomationFrameworkInstance(
            Func<Task<Browser>> driverConstructor,
            IJavaScriptTunnel domTunnel)
        {
            _driverConstructor = driverConstructor;
            _domTunnel = domTunnel;
        }

        public string UserAgentName => nameof(Puppeteer);

        public bool IsNavigating => _pendingNavigationRequestCount > 0;

        private async Task<ElementHandle[]> GetElementHandlesFromDomElementsAsync(IReadOnlyList<IDomElement> domElements)
        {
            var selector = domElements
                .Select(x => x.CssSelector)
                .Aggregate(string.Empty, (a, b) => $"{a}, {b}")
                .TrimStart(',', ' ');

            return await _page.QuerySelectorAllAsync(selector);
        }

        public async Task ClickAsync(IReadOnlyList<IDomElement> elements, int? offsetX, int? offsetY)
        {
            foreach (var element in elements)
            {
                await _page.Mouse.ClickAsync(
                    (element.BoundingClientRectangle.Left + offsetX) ?? element.BoundingClientRectangle.Center.X,
                    (element.BoundingClientRectangle.Top + offsetY) ?? element.BoundingClientRectangle.Center.Y);
            }
        }

        public Task DisposeAsync()
        {
            _isDisposed = true;

            if (_page != null)
            {
                _page.Request -= PageRequest;
                _page.RequestFinished -= PageRequestFinished;
                _page.RequestFailed -= PageRequestFinished;

                _page?.Dispose();
            }

            _browser?.Dispose();

            return Task.CompletedTask;
        }

        public async Task DoubleClickAsync(IReadOnlyList<IDomElement> elements, int? offsetX, int? offsetY)
        {
            foreach (var element in elements)
            {
                await _page.Mouse.ClickAsync(
                    (element.BoundingClientRectangle.Left + offsetX) ?? element.BoundingClientRectangle.Center.X,
                    (element.BoundingClientRectangle.Top + offsetY) ?? element.BoundingClientRectangle.Center.Y,
                    new ClickOptions()
                    {
                        ClickCount = 2
                    });
            }
        }

        public async Task DragDropAsync(
            IDomElement from,
            int? fromOffsetX,
            int? fromOffsetY,
            IDomElement to,
            int? toOffsetX,
            int? toOffsetY)
        {
            var javascriptScope = _domTunnel.DeclareScope(this);
            try
            {
                var dataTransferObjectVariableName = await javascriptScope.CreateNewVariableAsync("new DataTransfer()");

                await _page.Mouse.MoveAsync(
                    (from.BoundingClientRectangle.Left + fromOffsetX) ?? from.BoundingClientRectangle.Center.X,
                    (from.BoundingClientRectangle.Top + fromOffsetY) ?? from.BoundingClientRectangle.Center.Y);
                await _page.Mouse.DownAsync();

                await _domTunnel.DispatchDomElementDragEventAsync(
                    this,
                    from,
                    "dragstart",
                    dataTransferObjectVariableName);

                await _domTunnel.DispatchDomElementDragEventAsync(
                    this,
                    from,
                    "drag",
                    dataTransferObjectVariableName);

                await _page.Mouse.MoveAsync(
                    (to.BoundingClientRectangle.Left + toOffsetX) ?? to.BoundingClientRectangle.Center.X,
                    (to.BoundingClientRectangle.Top + toOffsetY) ?? to.BoundingClientRectangle.Center.Y);

                await _domTunnel.DispatchDomElementDragEventAsync(
                    this,
                    to,
                    "dragenter",
                    dataTransferObjectVariableName);

                await _domTunnel.DispatchDomElementDragEventAsync(
                    this,
                    to,
                    "dragover",
                    dataTransferObjectVariableName);

                await _domTunnel.DispatchDomElementDragEventAsync(
                    this,
                    from,
                    "drag",
                    dataTransferObjectVariableName);

                await _page.Mouse.UpAsync();

                await _domTunnel.DispatchDomElementDragEventAsync(
                    this,
                    to,
                    "dragleave",
                    dataTransferObjectVariableName);

                await _domTunnel.DispatchDomElementDragEventAsync(
                    this,
                    from,
                    "dragend",
                    dataTransferObjectVariableName);

                await _domTunnel.DispatchDomElementDragEventAsync(
                    this,
                    to,
                    "drop",
                    dataTransferObjectVariableName);
            }
            finally
            {
                await javascriptScope.DeleteAllVariablesAsync();
            }
        }

        public async Task EnterTextInAsync(IReadOnlyList<IDomElement> elements, string text)
        {
            var handles = await GetElementHandlesFromDomElementsAsync(elements);
            foreach (var handle in handles)
            {
                await handle.TypeAsync(text);
            }
        }

        public async Task<string> EvaluateJavaScriptExpressionAsync(string code)
        {
            while (true)
            {
                try
                {
                    while (IsNavigating)
                        await Task.Delay(100);

                    var blob = await _page.EvaluateExpressionAsync(code);
                    return blob?.ToString();
                }
                catch (PuppeteerException)
                {
                    if(!IsNavigating)
                        throw;
                }
            }
        }

        public async Task<IReadOnlyList<IDomElement>> FindDomElementsBySelectorAsync(int methodChainOffset, string selector)
        {
            if (_isDisposed)
                return new List<IDomElement>();

            return await _domTunnel.GetDomElementsFromSelector(this,
                methodChainOffset,
                selector);
        }

        public async Task FocusAsync(IDomElement domElement)
        {
            var handle = await GetElementHandleFromDomElementAsync(domElement);
            await handle.FocusAsync();

            //there is a bug causing Puppeteer to not focus input elements properly - so we invoke their onfocus events manually.
            if (domElement.TagName == "INPUT" && domElement.Attributes["type"] == "text")
            {
                await _domTunnel.DispatchDomElementFocusEventAsync(
                    this,
                    domElement,
                    "focus");
            }
        }

        private async Task<ElementHandle> GetElementHandleFromDomElementAsync(IDomElement domElement)
        {
            return await _page.QuerySelectorAsync(domElement.CssSelector);
        }

        public async Task HoverAsync(IDomElement domElement, int? offsetX, int? offsetY)
        {
            await _page.Mouse.MoveAsync(
                (domElement.BoundingClientRectangle.Left + offsetX) ?? domElement.BoundingClientRectangle.Center.X,
                (domElement.BoundingClientRectangle.Top + offsetY) ?? domElement.BoundingClientRectangle.Center.Y);
        }

        public async Task InitializeAsync()
        {
            _browser = await _driverConstructor();

            var pages = await _browser.PagesAsync();
            _page = pages.Single();

            await _page.SetCacheEnabledAsync(false);

            _page.Request += PageRequest;
            _page.RequestFinished += PageRequestFinished;
            _page.RequestFailed += PageRequestFinished;
        }

        private void PageRequestFinished(object sender, RequestEventArgs e)
        {
            if (!e.Request.IsNavigationRequest)
                return;

            Interlocked.Decrement(ref _pendingNavigationRequestCount);
        }

        private void PageRequest(object sender, RequestEventArgs e)
        {
            if (!e.Request.IsNavigationRequest)
                return;

            Interlocked.Increment(ref _pendingNavigationRequestCount);
        }

        public async Task OpenAsync(string uri)
        {
            await _page.GoToAsync(uri);
        }

        public async Task RightClickAsync(IReadOnlyList<IDomElement> elements, int? offsetX, int? offsetY)
        {
            foreach (var element in elements)
            {
                await _page.Mouse.ClickAsync(
                    (element.BoundingClientRectangle.Left + offsetX) ?? element.BoundingClientRectangle.Center.X,
                    (element.BoundingClientRectangle.Top + offsetY) ?? element.BoundingClientRectangle.Center.Y,
                    new ClickOptions()
                    {
                        Button = MouseButton.Right
                    });
            }
        }

        public async Task SelectByIndicesAsync(IReadOnlyList<IDomElement> elements, int[] byIndices)
        {
            foreach (var element in elements)
            {
                var selector = byIndices
                    .Select(x => $"{element.CssSelector} > option:nth-child({x + 1})")
                    .Aggregate(string.Empty, (a, b) => $"{a}, {b}")
                    .TrimStart(',', ' ');
                var handles = await _page.QuerySelectorAllAsync(selector);
                var valueTasks = handles.Select(x => _page.EvaluateFunctionAsync("x => x.value", x));
                var valueTokens = await Task.WhenAll(valueTasks);
                var values = valueTokens
                    .Cast<JValue>()
                    .Select(x => x.Value)
                    .Cast<string>();
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
                    .Aggregate(string.Empty, (a, b) => $"{a}, {b}")
                    .TrimStart(',', ' ');
                var handles = await _page.QuerySelectorAllAsync(selector);
                var tasks = handles.Select(x => _page.EvaluateFunctionAsync("x => { return { value: x.value, textContent: x.textContent } }", x));
                var tokens = await Task.WhenAll(tasks);
                var values = tokens
                    .Select(x => new
                    {
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
                .Aggregate(string.Empty, (a, b) => $"{a}, {b}")
                .TrimStart(',', ' ');
            await _page.SelectAsync(selector, byValues);
        }

        public async Task<SKBitmap> TakeScreenshotAsync()
        {
            var bytes = await _page.ScreenshotDataAsync(new ScreenshotOptions()
            {
                FullPage = true,
                Quality = 100,
                Type = ScreenshotType.Jpeg
            });
            return SKBitmap.Decode(bytes);
        }

        public async Task<IReadOnlyList<IDomElement>> FindDomElementsByCssSelectorsAsync(int methodChainOffset, string[] selectors)
        {
            return await _domTunnel.FindDomElementsByCssSelectorsAsync(this,
                methodChainOffset,
                selectors);
        }
    }
}
