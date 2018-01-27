using FluffySpoon.Automation.Web.Fluent.Targets;
using System.Threading.Tasks;

namespace FluffySpoon.Automation.Web.Fluent.RightClick
{
	class RightClickMethodChainNode : BaseMouseTargetsMethodChainNode<IBaseMethodChainNode, RightClickMethodChainNode, RightClickOnTargetsMethodChainNode>
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
