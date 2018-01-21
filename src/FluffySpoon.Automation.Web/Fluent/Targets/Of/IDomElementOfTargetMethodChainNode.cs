using FluffySpoon.Automation.Web.Dom;

namespace FluffySpoon.Automation.Web.Fluent.Targets.Of
{
	public interface IDomElementOfTargetMethodChainNode<out TCurrentMethodChainNode, out TNextMethodChainNode> : ITargetMethodChainNode<TCurrentMethodChainNode, TNextMethodChainNode>
		where TNextMethodChainNode : IBaseMethodChainNode
		where TCurrentMethodChainNode : IBaseMethodChainNode
    {
		TNextMethodChainNode Of(string selector);
		TNextMethodChainNode Of(IDomElement element);
	}
}
