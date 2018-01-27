using FluffySpoon.Automation.Web.Fluent.Targets;
using System.Threading.Tasks;

namespace FluffySpoon.Automation.Web.Fluent.Drag
{
	class DragMethodChainNode : BaseMouseTargetMethodChainNode<DragMethodChainNode, DragFromTargetMethodChainNode>
	{
		public DragMethodChainNode()
		{

		}

		protected override Task OnExecuteAsync(IWebAutomationFrameworkInstance framework)
		{
			return base.OnExecuteAsync(framework);
		}
	}
}
