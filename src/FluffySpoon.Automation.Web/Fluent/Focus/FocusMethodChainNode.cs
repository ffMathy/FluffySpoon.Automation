using FluffySpoon.Automation.Web.Fluent.Targets;

namespace FluffySpoon.Automation.Web.Fluent.Focus
{
	class FocusMethodChainNode : BaseDomElementTargetMethodChainNode<IBaseMethodChainNode, FocusMethodChainNode, FocusOnTargetMethodChainNode>
	{
		public override IBaseMethodChainNode Clone()
		{
			var node = new FocusMethodChainNode();
			TransferDelegation(node);

			return node;
		}
	}
}
