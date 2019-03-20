using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace FluffySpoon.Automation.Web.Dom
{
	class JavaScriptTunnel : IJavaScriptTunnel
	{
		private readonly IDomSelectorStrategy _domSelectorStrategy;

		public JavaScriptTunnel(
			IDomSelectorStrategy domSelectorStrategy)
		{
			_domSelectorStrategy = domSelectorStrategy;
		}

		public async Task<IReadOnlyList<IDomElement>> GetDomElementsFromSelector(
			IWebAutomationFrameworkInstance automationFrameworkInstance,
			int methodChainOffset,
			string selector)
		{
			var scriptToExecute = 
				_domSelectorStrategy.DomSelectorLibraryJavaScript + "; " + 
				_domSelectorStrategy.GetJavaScriptForRetrievingDomElements(selector);
			return await GetDomElementsFromJavaScriptCode(
				automationFrameworkInstance, 
				methodChainOffset, 
				scriptToExecute);
		}

		public async Task<IReadOnlyList<IDomElement>> FindDomElementsByCssSelectorsAsync(
			IWebAutomationFrameworkInstance automationFrameworkInstance,
			int methodChainOffset,
			string[] selectors)
        {
            var findElementsScript = GenerateFindElementsScriptFromCssSelectors(selectors);
            return await GetDomElementsFromJavaScriptCode(
                automationFrameworkInstance,
                methodChainOffset,
                $"return {findElementsScript}");
        }

        private string GenerateFindElementsScriptFromCssSelectors(params string[] selectors)
        {
            var combinedSelector = selectors
                .Aggregate((a, b) => a + ", " + b)
                .TrimStart(',', ' ');
            var sanitizedSelector = combinedSelector.Replace("'", "\\'");
            return $@"document.querySelectorAll('{sanitizedSelector}')";
        }

        public async Task DispatchDomElementDragEventAsync(
            IWebAutomationFrameworkInstance automationFrameworkInstance,
            IDomElement domElement,
            string eventName,
            string dataTransferExpression)
        {
            var findElementsScript = GenerateFindElementsScriptFromCssSelectors(domElement.CssSelector);

            await automationFrameworkInstance.EvaluateJavaScriptAsync(
                WrapJavaScriptInIsolatedFunction($@"
                    var e;
                    try {{
                        e = new DragEvent('{eventName}', {{ dataTransfer: {dataTransferExpression} }});
                    }} catch(ex) {{
                        e = document.createEvent('DragEvent');
                        e.initDragEvent('{eventName}', true, true, null, null, null, null, null, null, false, false, false, false, null, null, {dataTransferExpression});
                    }}

                    var element = [...{findElementsScript}][0];
                    element.dispatchEvent(e);
                "));
        }

        public async Task DispatchDomElementFocusEventAsync(
            IWebAutomationFrameworkInstance automationFrameworkInstance,
            IDomElement domElement,
            string eventName)
        {
            var findElementsScript = GenerateFindElementsScriptFromCssSelectors(domElement.CssSelector);

            await automationFrameworkInstance.EvaluateJavaScriptAsync(
                WrapJavaScriptInIsolatedFunction($@"
                    var e = new FocusEvent('{eventName}');

                    var element = [...{findElementsScript}][0];
                    element.dispatchEvent(e);
                "));
        }

        public async Task<IReadOnlyList<IDomElement>> GetDomElementsFromJavaScriptCode(
			IWebAutomationFrameworkInstance automationFrameworkInstance, 
			int methodChainOffset, 
			string scriptToExecute)
        {
            var finalScriptToExecute = WrapJavaScriptInIsolatedFunction($@"
				var elements = {WrapJavaScriptInIsolatedFunction(scriptToExecute)};
				var returnValues = [];

				for(var i = 0; i < elements.length; i++) {{
					var element = elements[i];

					var attributes = [];
					var computedStyleProperties = [];

					var tag = element.getAttribute('data-fluffyspoon-tag') || '{methodChainOffset}-'+i;
					element.setAttribute('data-fluffyspoon-tag', tag);

					var o;

					for(o = 0; o < element.attributes.length; o++) {{
						var attribute = element.attributes[o];
						attributes.push({{
							name: attribute.name,
							value: attribute.value
						}});
					}}

					var computedStyle = getComputedStyle(element);
					for(o = 0; o < computedStyle.length; o++) {{
						var styleKey = computedStyle[o];
						computedStyleProperties.push({{
							property: styleKey,
							value: computedStyle.getPropertyValue(styleKey)
						}});
					}}

					var boundingClientRectangle = element.getBoundingClientRect();

					returnValues.push({{
						tag: tag,
						attributes: attributes,
						computedStyle: computedStyleProperties,
						textContent: element.textContent,
						innerText: element.innerText,
						value: element.value,
                        tagName: element.tagName,
						clientLeft: element.clientLeft,
						clientTop: element.clientTop,
						clientWidth: element.clientWidth,
						clientHeight: element.clientHeight,
                        updatedAt: new Date(),
						boundingClientRectangle: {{
							left: boundingClientRectangle.left,
							right: boundingClientRectangle.right,
							top: boundingClientRectangle.top,
							bottom: boundingClientRectangle.bottom
						}}
					}});
				}}

				return JSON.stringify(returnValues);
			");

            var resultJsonBlobs = await automationFrameworkInstance.EvaluateJavaScriptAsync(finalScriptToExecute);
            Debug.Assert(resultJsonBlobs != null, "result json blobs not null");

            var blobs = JsonConvert.DeserializeObject<ElementWrapper[]>(resultJsonBlobs);
            return blobs
                .Select(x =>
                {
                    var attributes = new DomAttributes();
                    foreach (var attribute in x.Attributes)
                        attributes.Add(attribute.Name, attribute.Value);

                    var computedStyle = new DomStyle(x.ComputedStyle);

                    var domElement = new DomElement(
                        cssSelector: "[data-fluffyspoon-tag='" + x.Tag + "']",
                        textContent: x.TextContent,
                        innerText: x.InnerText,
                        value: x.Value,
                        tagName: x.TagName,
                        clientLeft: x.ClientLeft,
                        clientTop: x.ClientTop,
                        clientWidth: x.ClientWidth,
                        clientHeight: x.ClientHeight,
                        boundingClientRectangle: x.BoundingClientRectangle,
                        attributes: attributes,
                        computedStyle: computedStyle,
                        updatedAt: x.UpdatedAt);

                    return domElement;
                })
                .ToArray();
        }

        public string WrapJavaScriptInIsolatedFunction(string code)
		{
			return $"((function() {{{code}}})())";
		}

        public IJavaScriptScope DeclareScope(IWebAutomationFrameworkInstance automationFrameworkInstance)
        {
            return new JavaScriptScope(
                automationFrameworkInstance, 
                this);
        }

        private class ElementWrapper
		{
			public string Tag { get; set; }
			public string TextContent { get; set; }
			public string InnerText { get; set; }
			public string Value { get; set; }
            public string TagName { get; set; }

			public int ClientLeft { get; set; }
			public int ClientTop { get; set; }
			public int ClientWidth { get; set; }
			public int ClientHeight { get; set; }

			public DomRectangle BoundingClientRectangle { get; set; }

			public DomAttribute[] Attributes { get; set; }
			public DomStyleProperty[] ComputedStyle { get; set; }

            public DateTime UpdatedAt { get; set; }
		}
	}
}
