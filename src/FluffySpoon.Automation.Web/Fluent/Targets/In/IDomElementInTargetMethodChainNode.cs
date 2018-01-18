using FluffySpoon.Automation.Web.Dom;

namespace FluffySpoon.Automation.Web.Fluent.Targets.In
{
	public interface IDomElementInTargetMethodChainNode<TNextMethodChainNode> : ITargetMethodChainNode where TNextMethodChainNode : IBaseMethodChainNode
    {
		TNextMethodChainNode In(string selector);
		TNextMethodChainNode In(IDomElement element);
	}
}
