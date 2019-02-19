using FluffySpoon.Automation.Web.Fluent.Targets;

namespace FluffySpoon.Automation.Web.Fluent.Click
{
	class ClickMethodChainNode : BaseMouseTargetsMethodChainNode<IBaseMethodChainNode, ClickMethodChainNode, ClickOnTargetsMethodChainNode>
	{
		public override IBaseMethodChainNode Clone()
		{
			var clone = new ClickMethodChainNode();
			TransferDelegation(clone);

			return clone;
		}
	}
}
