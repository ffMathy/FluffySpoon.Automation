using FluffySpoon.Automation.Web.Fluent.Targets;

namespace FluffySpoon.Automation.Web.Fluent.Drag
{
	class DragFromTargetMethodChainNode : BaseMouseTargetMethodChainNode<DragMethodChainNode, DragFromTargetMethodChainNode, DragFromTargetToTargetMethodChainNode>
	{
		public override IBaseMethodChainNode Clone()
		{
			var clone = new DragFromTargetMethodChainNode();
			TransferDelegation(clone);

			return clone;
		}
	}
}
