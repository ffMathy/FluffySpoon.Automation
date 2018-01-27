using FluffySpoon.Automation.Web.Fluent.Targets;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace FluffySpoon.Automation.Web.Fluent.RightClick
{
    class RightClickMethodChainNode : BaseMouseTargetsMethodChainNode<RightClickMethodChainNode, RightClickOnTargetsMethodChainNode>
	{
		public RightClickMethodChainNode()
		{

		}

		protected override Task OnExecuteAsync(IWebAutomationFrameworkInstance framework)
		{
			return base.OnExecuteAsync(framework);
		}
	}
}
