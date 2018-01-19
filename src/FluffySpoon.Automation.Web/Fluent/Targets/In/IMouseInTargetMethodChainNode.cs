using FluffySpoon.Automation.Web.Dom;

namespace FluffySpoon.Automation.Web.Fluent.Targets.In
{
	public interface IMouseInTargetMethodChainNode<out TCurrentMethodChainNode, out TNextMethodChainNode> : IDomElementInTargetMethodChainNode<TCurrentMethodChainNode, TNextMethodChainNode> 
		where TNextMethodChainNode : IBaseMethodChainNode<TCurrentMethodChainNode>
		where TCurrentMethodChainNode : IBaseMethodChainNode
	{
		TNextMethodChainNode In(int x, int y);
		TNextMethodChainNode In(IDomElement element, int relativeX, int relativeY);
	}
}
