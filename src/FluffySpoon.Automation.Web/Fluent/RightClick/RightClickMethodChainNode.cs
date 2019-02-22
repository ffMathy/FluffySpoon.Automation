using FluffySpoon.Automation.Web.Fluent.Targets;

namespace FluffySpoon.Automation.Web.Fluent.RightClick
{
	class RightClickMethodChainNode : BaseMouseTargetsMethodChainNode<IBaseMethodChainNode, RightClickMethodChainNode, RightClickOnTargetsMethodChainNode>
	{
		public override IBaseMethodChainNode Clone()
		{
			var node = new RightClickMethodChainNode();
            TransferDelegation(node);

            return node;
        }
	}
}
