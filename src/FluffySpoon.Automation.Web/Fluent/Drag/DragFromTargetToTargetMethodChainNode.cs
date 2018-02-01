using FluffySpoon.Automation.Web.Fluent.Root;
using System.Linq;
using System.Threading.Tasks;

namespace FluffySpoon.Automation.Web.Fluent.Drag
{
	internal class DragFromTargetToTargetMethodChainNode : MethodChainRoot<DragFromTargetMethodChainNode>, IDragFromTargetToTargetMethodChainNode
	{
		protected override async Task OnExecuteAsync(IWebAutomationFrameworkInstance framework)
		{
			var fromNode = Parent.Parent;
			var toNode = Parent;

			await framework.DragDropAsync(
				fromNode.Elements.Single(),
				fromNode.OffsetX,
				fromNode.OffsetY,
				toNode.Elements.Single(),
				toNode.OffsetX,
				toNode.OffsetY);
			await base.OnExecuteAsync(framework);
		}
	}
}