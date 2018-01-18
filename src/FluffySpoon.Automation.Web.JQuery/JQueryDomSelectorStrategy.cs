using FluffySpoon.Automation.Web.Dom;
using FluffySpoon.Http;
using System;
using System.Threading.Tasks;

namespace FluffySpoon.Automation.Web.JQuery
{
	class JQueryDomSelectorStrategy : IDomSelectorStrategy
	{
		private readonly IRestClient _webClient;

		private string _uniqueJQueryInstanceReference;

		public string InitialJavaScriptForEachPage { get; private set; }

		public JQueryDomSelectorStrategy()
		{
			_webClient = new RestClient();

			_uniqueJQueryInstanceReference = "window['fluffyspoon-jquery-" + Guid.NewGuid() + "']";
		}

		public string GetJavaScriptForRetrievingDomElements(string selector)
		{
			var sanitizedSelector = PrepareSelectorForInlining(selector);
			return "return " + _uniqueJQueryInstanceReference + "('" + sanitizedSelector + "').get()";
		}

		public async Task InitializeAsync()
		{
			var jQueryScriptContents = await _webClient.GetAsync(new Uri("https://ajax.googleapis.com/ajax/libs/jquery/3.2.1/jquery.min.js"));
			InitialJavaScriptForEachPage = _uniqueJQueryInstanceReference + @"=(function() {" + jQueryScriptContents + @"})()||jQuery.noConflict()";
		}

		private static string PrepareSelectorForInlining(string selector)
		{
			return selector.Replace("'", "\\'");
		}
	}
}
