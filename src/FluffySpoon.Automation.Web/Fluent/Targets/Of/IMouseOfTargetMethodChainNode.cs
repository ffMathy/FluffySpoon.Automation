using FluffySpoon.Automation.Web.Dom;

namespace FluffySpoon.Automation.Web.Fluent.Targets.Of
{
	public interface IMouseOfTargetMethodChainNode<TNextMethodChainNode> : IDomElementOfTargetMethodChainNode<TNextMethodChainNode> where TNextMethodChainNode : IBaseMethodChainNode
	{
		TNextMethodChainNode Of(int x, int y);
		TNextMethodChainNode Of(IDomElement element, int relativeX, int relativeY);
	}
}
