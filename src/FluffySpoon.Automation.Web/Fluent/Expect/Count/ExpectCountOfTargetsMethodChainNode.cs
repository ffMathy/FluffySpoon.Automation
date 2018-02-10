using FluffySpoon.Automation.Web.Dom;
using FluffySpoon.Automation.Web.Exceptions;
using FluffySpoon.Automation.Web.Fluent.Expect.Root;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FluffySpoon.Automation.Web.Fluent.Expect.Count
{
	class ExpectCountOfTargetsMethodChainNode : ExpectMethodChainRoot<ExpectCountMethodChainNode>, IExpectCountOfTargetsMethodChainNode
	{
		public override IReadOnlyList<IDomElement> Elements
		{
			get => Parent?.Elements;
		}

		public ExpectCountOfTargetsMethodChainNode()
		{

		}

		protected override async Task OnExecuteAsync(IWebAutomationFrameworkInstance framework)
		{
			if(Elements?.Count != Parent.Count)
				throw ExpectationNotMetException.FromMethodChainNode(this, "Expected " + Parent.Count + " elements but found " + (Parent.Elements?.Count ?? 0) + ".");

			await base.OnExecuteAsync(framework);
		}

		public override IBaseMethodChainNode Clone()
		{
			return new ExpectCountOfTargetsMethodChainNode();
		}
	}
}
