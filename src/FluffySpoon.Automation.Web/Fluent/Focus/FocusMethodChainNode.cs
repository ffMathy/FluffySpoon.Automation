using FluffySpoon.Automation.Web.Fluent.Targets;
using System.Threading.Tasks;

namespace FluffySpoon.Automation.Web.Fluent.Focus
{
	class FocusMethodChainNode : BaseMouseTargetMethodChainNode<IBaseMethodChainNode, FocusMethodChainNode, FocusOnTargetMethodChainNode>
	{
		public FocusMethodChainNode()
		{

		}

		protected override Task OnExecuteAsync(IWebAutomationFrameworkInstance framework)
		{
			return base.OnExecuteAsync(framework);
		}
	}
}
