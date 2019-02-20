using FluffySpoon.Automation.Web.Exceptions;
using FluffySpoon.Automation.Web.Fluent.Root;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace FluffySpoon.Automation.Web.Fluent.Drag
{
	internal class DragFromTargetToTargetMethodChainNode : MethodChainRoot<DragFromTargetMethodChainNode>, IDragFromTargetToTargetMethodChainNode
	{
		protected override bool MayCauseElementSideEffects => true;

		protected override async Task OnExecuteAsync(IWebAutomationFrameworkInstance framework)
		{
			var fromNode = Parent.Parent;
			var toNode = Parent;

			if (fromNode.Elements.Count == 0)
				throw new AutomationException("The drag/drop operation failed because the \"from\" selector did not match any element.");

			if (toNode.Elements.Count == 0)
				throw new AutomationException("The drag/drop operation failed because the \"to\" selector did not match any element.");

			if (fromNode.Elements.Count > 1)
				throw new AutomationException("The drag/drop operation failed because the \"from\" selector matched more than one element.");

			if (toNode.Elements.Count > 1)
				throw new AutomationException("The drag/drop operation failed because the \"to\" selector matched more than one element.");

			await framework.DragDropAsync(
				fromNode.Elements.Single(),
				fromNode.OffsetX,
				fromNode.OffsetY,
				toNode.Elements.Single(),
				toNode.OffsetX,
				toNode.OffsetY);
			await base.OnExecuteAsync(framework);
		}

		public override IBaseMethodChainNode Clone()
		{
			return new DragFromTargetToTargetMethodChainNode();
		}
	}
}