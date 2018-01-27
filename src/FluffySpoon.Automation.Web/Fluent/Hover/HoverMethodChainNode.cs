using FluffySpoon.Automation.Web.Fluent.Targets;
using System.Threading.Tasks;

namespace FluffySpoon.Automation.Web.Fluent.Hover
{
	class HoverMethodChainNode : BaseMouseTargetMethodChainNode<HoverMethodChainNode, HoverOnTargetMethodChainNode>
	{
		public HoverMethodChainNode()
		{

		}

		protected override Task OnExecuteAsync(IWebAutomationFrameworkInstance framework)
		{
			return base.OnExecuteAsync(framework);
		}
	}
}
