using FluffySpoon.Automation.Web.Dom;

namespace FluffySpoon.Automation.Web.Fluent.Targets.From
{
	public interface IMouseFromTargetMethodChainNode<out TNextMethodChainNode> : IDomElementFromTargetMethodChainNode<TNextMethodChainNode> where TNextMethodChainNode : IBaseMethodChainNode
	{
		TNextMethodChainNode From(int x, int y);
		TNextMethodChainNode From(IDomElement element, int relativeX, int relativeY);
	}
}
