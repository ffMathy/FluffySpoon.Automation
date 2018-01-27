using FluffySpoon.Automation.Web.Dom;

namespace FluffySpoon.Automation.Web.Fluent.Targets.To
{
	public interface IMouseToTargetMethodChainNode<out TCurrentMethodChainNode, out TNextMethodChainNode> : IDomElementToTargetMethodChainNode<TCurrentMethodChainNode, TNextMethodChainNode> 
		where TNextMethodChainNode : IBaseMethodChainNode
		where TCurrentMethodChainNode : IBaseMethodChainNode
	{
		TNextMethodChainNode To(int x, int y);
		TNextMethodChainNode To(IDomElement element, int relativeX, int relativeY);
	}
}
