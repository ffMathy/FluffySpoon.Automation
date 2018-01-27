using FluffySpoon.Automation.Web.Fluent.Targets;
using System.Threading.Tasks;

namespace FluffySpoon.Automation.Web.Fluent.DoubleClick
{
	class DoubleClickMethodChainNode : BaseMouseTargetsMethodChainNode<DoubleClickMethodChainNode, DoubleClickOnTargetsMethodChainNode>
	{
		public DoubleClickMethodChainNode()
		{

		}

		protected override Task OnExecuteAsync(IWebAutomationFrameworkInstance framework)
		{
			return base.OnExecuteAsync(framework);
		}
	}
}
