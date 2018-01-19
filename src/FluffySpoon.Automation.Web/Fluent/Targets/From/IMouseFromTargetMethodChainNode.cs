using FluffySpoon.Automation.Web.Dom;

namespace FluffySpoon.Automation.Web.Fluent.Targets.From
{
	public interface IMouseFromTargetMethodChainNode<out TCurrentMethodChainNode, out TNextMethodChainNode> : IDomElementFromTargetMethodChainNode<TCurrentMethodChainNode, TNextMethodChainNode> 
		where TNextMethodChainNode : IBaseMethodChainNode<TCurrentMethodChainNode>
		where TCurrentMethodChainNode : IBaseMethodChainNode
	{
		TNextMethodChainNode From(int x, int y);
		TNextMethodChainNode From(IDomElement element, int relativeX, int relativeY);
	}
}
