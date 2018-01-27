using FluffySpoon.Automation.Web.Dom;

namespace FluffySpoon.Automation.Web.Fluent.Targets.To
{
	public interface IDomElementToTargetMethodChainNode<out TCurrentMethodChainNode, out TNextMethodChainNode> : ITargetMethodChainNode<TCurrentMethodChainNode, TNextMethodChainNode> 
		where TNextMethodChainNode : IBaseMethodChainNode
		where TCurrentMethodChainNode : IBaseMethodChainNode
    {
		TNextMethodChainNode To(string selector);
		TNextMethodChainNode To(IDomElement element);
	}
}
