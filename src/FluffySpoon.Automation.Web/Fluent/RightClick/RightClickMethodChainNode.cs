using FluffySpoon.Automation.Web.Fluent.Targets;

namespace FluffySpoon.Automation.Web.Fluent.RightClick
{
	class RightClickMethodChainNode : BaseMouseTargetsMethodChainNode<IBaseMethodChainNode, RightClickMethodChainNode, RightClickOnTargetsMethodChainNode>
	{
		public override IBaseMethodChainNode Clone()
		{
			return new RightClickMethodChainNode();
		}
	}
}
