using FluffySpoon.Automation.Web.Dom;
using FluffySpoon.Automation.Web.Exceptions;
using FluffySpoon.Automation.Web.Fluent.Expect.Root;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FluffySpoon.Automation.Web.Fluent.Expect.Exists
{
	class ExpectExistsMethodChainNode : ExpectMethodChainRoot<IBaseMethodChainNode>, IExpectExistsMethodChainNode
	{
		private readonly string _selector;

		private readonly IReadOnlyList<IDomElement> _domElements;

		public ExpectExistsMethodChainNode(string selector) {
			_selector = selector;
		}

		public ExpectExistsMethodChainNode(IReadOnlyList<IDomElement> domElements)
		{
			_domElements = domElements;
		}

		protected override async Task OnExecuteAsync(IWebAutomationFrameworkInstance framework)
		{
			IReadOnlyList<IDomElement> elements = null;
			if(_selector != null)
				elements = await framework.FindDomElementsBySelectorAsync(_selector);

			if (_domElements != null)
				elements = await framework.FindDomElementsByCssSelectorsAsync(_domElements
					.Select(x => x.CssSelector)
					.ToArray());

			if (elements == null || elements.Count == 0)
				throw ExpectationNotMetException.FromMethodChainNode(this, "No matching elements were found.");
		}
	}
}
