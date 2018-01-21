using FluffySpoon.Automation.Web.Exceptions;
using System.Threading.Tasks;

namespace FluffySpoon.Automation.Web.Fluent.Expect.Exists
{
	class ExpectExistsMethodChainNode : BaseMethodChainNode<IBaseMethodChainNode>, IExpectExistsMethodChainNode
	{
		private readonly string _selector;

		public ExpectExistsMethodChainNode(string selector) {
			_selector = selector;
		}

		protected override async Task OnExecuteAsync(IWebAutomationFrameworkInstance framework)
		{
			var elements = await framework.FindDomElementsAsync(_selector);
			if (elements.Count == 0)
				throw ExpectationNotMetException.FromMethodChainNode(this, "No matching elements were found.");
		}
	}
}
