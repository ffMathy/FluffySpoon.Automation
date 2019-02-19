using Newtonsoft.Json;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace FluffySpoon.Automation.Web.Dom
{
	class DomTunnel : IDomTunnel
	{
		private readonly IDomSelectorStrategy _domSelectorStrategy;

		public DomTunnel(
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

		public async Task<IReadOnlyList<IDomElement>> GetDomElementsFromJavaScriptCode(
			IWebAutomationFrameworkInstance automationFrameworkInstance, 
			int methodChainOffset, 
			string scriptToExecute)
		{
			var elementFetchJavaScript = WrapJavaScriptInIsolatedFunction(
				scriptToExecute);
				
			var resultJsonBlobs = await automationFrameworkInstance.EvaluateJavaScriptAsync(
				WrapJavaScriptInIsolatedFunction(@"
					var elements = " + elementFetchJavaScript + @";
					var returnValues = [];

					for(var i = 0; i < elements.length; i++) {
						var element = elements[i];

						var attributes = [];
						var computedStyleProperties = [];

						var tag = element.getAttribute('fluffyspoon-tag') || '" + methodChainOffset + @"-'+i;
						element.setAttribute('fluffyspoon-tag', tag);

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

						returnValues.push({
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
						});
					}

					return JSON.stringify(returnValues);
				"));

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
						cssSelector: "[fluffyspoon-tag='" + x.Tag + "']",
						textContent: x.TextContent,
						value: x.Value,
						clientLeft: x.ClientLeft,
						clientTop: x.ClientTop,
						clientWidth: x.ClientWidth,
						clientHeight: x.ClientHeight,
						boundingClientRectangle: x.BoundingClientRectangle,
						attributes: attributes,
						computedStyle: computedStyle);

					return domElement;
				})
				.ToArray();
		}

		private string WrapJavaScriptInIsolatedFunction(string code)
		{
			return $"(function() {{{code}}})();";
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
