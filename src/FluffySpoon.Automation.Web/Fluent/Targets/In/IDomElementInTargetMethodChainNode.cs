using FluffySpoon.Automation.Web.Dom;

namespace FluffySpoon.Automation.Web.Fluent.Targets.In
{
	public interface IDomElementInTargetMethodChainNode<out TCurrentMethodChainNode, out TNextMethodChainNode> : 
		ITargetMethodChainNode<TCurrentMethodChainNode, TNextMethodChainNode> 
		where TNextMethodChainNode : IBaseMethodChainNode<TCurrentMethodChainNode>
		where TCurrentMethodChainNode : IBaseMethodChainNode
    {
		TNextMethodChainNode In(string selector);
		TNextMethodChainNode In(IDomElement element);
	}
}
