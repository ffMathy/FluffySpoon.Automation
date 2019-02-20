using FluffySpoon.Automation.Web.Dom;
using FluffySpoon.Http;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace FluffySpoon.Automation.Web.JQuery
{
	class JQueryDomSelectorStrategy : IDomSelectorStrategy
	{
		private readonly IRestClient _webClient;

		private string _uniqueJQueryInstanceReference;

		public string DomSelectorLibraryJavaScript { get; private set; }

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
			var jQueryScriptContents = await _webClient.GetAsync<string>(new Uri("https://cdnjs.cloudflare.com/ajax/libs/sizzle/2.3.3/sizzle.min.js"));
            var trimmedScript = jQueryScriptContents
                .Split('\n')
                .Select(x => x.Trim())
                .Where(x => !x.StartsWith("//"))
                .Aggregate(string.Empty, (a, b) => a + b)
                .Trim(';');
            DomSelectorLibraryJavaScript = _uniqueJQueryInstanceReference + @"=(" + trimmedScript + ")";
		}

		private static string PrepareSelectorForInlining(string selector)
		{
			return selector.Replace("'", "\\'");
		}
	}
}
