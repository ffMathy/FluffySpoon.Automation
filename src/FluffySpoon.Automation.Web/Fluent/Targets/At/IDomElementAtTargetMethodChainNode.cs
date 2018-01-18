using FluffySpoon.Automation.Web.Dom;

namespace FluffySpoon.Automation.Web.Fluent.Targets.At
{
	public interface IDomElementAtTargetMethodChainNode<TNextMethodChainNode> : ITargetMethodChainNode where TNextMethodChainNode : IBaseMethodChainNode
    {
		TNextMethodChainNode At(string selector);
		TNextMethodChainNode At(IDomElement element);
	}
}
