using FluffySpoon.Automation.Web.Dom;

using System;
using System.Threading.Tasks;

namespace FluffySpoon.Automation.Web.Css
{
	class CssSelectorStrategy : IDomSelectorStrategy
	{
        public string DomSelectorLibraryJavaScript => string.Empty;

		public string GetJavaScriptForRetrievingDomElements(string selector)
		{
			var sanitizedSelector = PrepareSelectorForInlining(selector);
			return "return document.querySelectorAll('" + sanitizedSelector + "')";
		}

		public async Task InitializeAsync()
		{
		}

		private static string PrepareSelectorForInlining(string selector)
		{
			return selector.Replace("'", "\\'");
		}
	}
}
