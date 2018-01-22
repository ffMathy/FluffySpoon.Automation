using FluffySpoon.Automation.Web.Fluent.Targets;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace FluffySpoon.Automation.Web.Fluent.Expect.Count
{
    class ExpectCountMethodChainNode : BaseDomElementTargetsMethodChainNode<ExpectCountMethodChainNode, ExpectCountOfTargetsMethodChainNode>
	{
		internal int Count { get; }

		public ExpectCountMethodChainNode(int count)
		{
			Count = count;
		}

		protected override Task OnExecuteAsync(IWebAutomationFrameworkInstance framework)
		{
			return base.OnExecuteAsync(framework);
		}
	}
}
