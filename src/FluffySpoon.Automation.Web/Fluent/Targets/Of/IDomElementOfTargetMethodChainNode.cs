using FluffySpoon.Automation.Web.Dom;

namespace FluffySpoon.Automation.Web.Fluent.Targets.Of
{
	public interface IDomElementOfTargetMethodChainNode<TNextMethodChainNode> : ITargetMethodChainNode where TNextMethodChainNode : IBaseMethodChainNode
    {
		TNextMethodChainNode Of(string selector);
		TNextMethodChainNode Of(IDomElement element);
	}
}
