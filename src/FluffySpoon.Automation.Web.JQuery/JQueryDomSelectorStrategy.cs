using FluffySpoon.Automation.Web.Dom;
using FluffySpoon.Http;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FluffySpoon.Automation.Web.JQuery
{
	class JQueryDomSelectorStrategy : IDomSelectorStrategy
	{
		private readonly IWebAutomationFrameworkInstance _framework;
		private readonly IRestClient _webClient;
		
		private static string _uniqueJQueryInstanceReference;

		static JQueryDomSelectorStrategy()
		{
			_uniqueJQueryInstanceReference = "window['fluffyspoon-jquery-" + Guid.NewGuid() + "']";
		}

		public JQueryDomSelectorStrategy(
			IWebAutomationFrameworkInstance framework)
		{
			_webClient = new RestClient();
			_framework = framework;
		}

		public async Task<IReadOnlyList<IDomElement>> GetDomElementsAsync(string selector)
		{
			await PrepareJQueryIncludesAsync();
			var sanitizedSelector = PrepareSelectorForInlining(selector);
			return await _framework.EvaluateJavaScriptAsDomElementsAsync(
				"return " + _uniqueJQueryInstanceReference + "('" + sanitizedSelector + "').get()");
		}

		private async Task PrepareJQueryIncludesAsync() {
			if (await IsJQueryScriptIncluded())
				return;

			var jQueryScriptContents = await _webClient.GetAsync(new Uri("https://ajax.googleapis.com/ajax/libs/jquery/3.2.1/jquery.min.js"));
			await _framework.EvaluateJavaScriptAsync(_uniqueJQueryInstanceReference + @"=(function() {" + jQueryScriptContents + @"})()||jQuery.noConflict();");
		}

		private async Task<bool> IsJQueryScriptIncluded()
		{
			var result = await _framework.EvaluateJavaScriptAsync("return " + _uniqueJQueryInstanceReference + "?\"" + _uniqueJQueryInstanceReference + "\":null");
			return result != null && result == _uniqueJQueryInstanceReference;
		}

		private static string PrepareSelectorForInlining(string selector)
		{
			return selector.Replace("'", "\\'");
		}
	}
}
