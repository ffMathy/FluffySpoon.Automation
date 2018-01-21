using FluffySpoon.Automation.Web.Dom;

namespace FluffySpoon.Automation.Web.Fluent.Targets.On
{
	public interface IDomElementOnTargetMethodChainNode<out TCurrentMethodChainNode, out TNextMethodChainNode> : ITargetMethodChainNode<TCurrentMethodChainNode, TNextMethodChainNode> 
		where TNextMethodChainNode : IBaseMethodChainNode
		where TCurrentMethodChainNode : IBaseMethodChainNode
    {
		TNextMethodChainNode On(string selector);
		TNextMethodChainNode On(IDomElement element);
	}
}
