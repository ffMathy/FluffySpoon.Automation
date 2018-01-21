using FluffySpoon.Automation.Web.Dom;

namespace FluffySpoon.Automation.Web.Fluent.Targets.From
{
	public interface IDomElementFromTargetMethodChainNode<out TCurrentMethodChainNode, out TNextMethodChainNode> : ITargetMethodChainNode<TCurrentMethodChainNode, TNextMethodChainNode> 
		where TNextMethodChainNode : IBaseMethodChainNode
		where TCurrentMethodChainNode : IBaseMethodChainNode
    {
		TNextMethodChainNode From(string selector);
		TNextMethodChainNode From(IDomElement element);
	}
}
