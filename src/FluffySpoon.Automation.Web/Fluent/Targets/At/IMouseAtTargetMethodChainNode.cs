using FluffySpoon.Automation.Web.Dom;

namespace FluffySpoon.Automation.Web.Fluent.Targets.At
{
	public interface IMouseAtTargetMethodChainNode<out TCurrentMethodChainNode, out TNextMethodChainNode> : IDomElementAtTargetMethodChainNode<TCurrentMethodChainNode, TNextMethodChainNode> 
		where TNextMethodChainNode : IBaseMethodChainNode<TCurrentMethodChainNode>
		where TCurrentMethodChainNode : IBaseMethodChainNode
	{
		TNextMethodChainNode At(int x, int y);
		TNextMethodChainNode At(IDomElement element, int relativeX, int relativeY);
	}
}
