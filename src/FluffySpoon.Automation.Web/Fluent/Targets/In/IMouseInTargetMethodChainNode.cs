using FluffySpoon.Automation.Web.Dom;

namespace FluffySpoon.Automation.Web.Fluent.Targets.In
{
	public interface IMouseInTargetMethodChainNode<TNextMethodChainNode> : IDomElementInTargetMethodChainNode<TNextMethodChainNode> where TNextMethodChainNode : IBaseMethodChainNode
	{
		TNextMethodChainNode In(int x, int y);
		TNextMethodChainNode In(IDomElement element, int relativeX, int relativeY);
	}
}
