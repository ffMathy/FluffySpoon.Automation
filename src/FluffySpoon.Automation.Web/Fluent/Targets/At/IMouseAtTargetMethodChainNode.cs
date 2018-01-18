using FluffySpoon.Automation.Web.Dom;

namespace FluffySpoon.Automation.Web.Fluent.Targets.At
{
	public interface IMouseAtTargetMethodChainNode<TNextMethodChainNode> : IDomElementAtTargetMethodChainNode<TNextMethodChainNode> where TNextMethodChainNode : IBaseMethodChainNode
	{
		TNextMethodChainNode At(int x, int y);
		TNextMethodChainNode At(IDomElement element, int relativeX, int relativeY);
	}
}
