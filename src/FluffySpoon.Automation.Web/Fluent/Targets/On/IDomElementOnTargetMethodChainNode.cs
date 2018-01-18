using FluffySpoon.Automation.Web.Dom;

namespace FluffySpoon.Automation.Web.Fluent.Targets.On
{
	public interface IDomElementOnTargetMethodChainNode<TNextMethodChainNode> : ITargetMethodChainNode where TNextMethodChainNode : IBaseMethodChainNode
    {
		TNextMethodChainNode On(string selector);
		TNextMethodChainNode On(IDomElement element);
	}
}
