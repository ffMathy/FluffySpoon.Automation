using FluffySpoon.Automation.Web.Dom;

namespace FluffySpoon.Automation.Web.Fluent.Targets.From
{
	public interface IDomElementFromTargetMethodChainNode<TNextMethodChainNode> : ITargetMethodChainNode where TNextMethodChainNode : IBaseMethodChainNode
    {
		TNextMethodChainNode From(string selector);
		TNextMethodChainNode From(IDomElement element);
	}
}
