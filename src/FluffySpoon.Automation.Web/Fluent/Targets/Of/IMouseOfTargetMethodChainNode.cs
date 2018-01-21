using FluffySpoon.Automation.Web.Dom;

namespace FluffySpoon.Automation.Web.Fluent.Targets.Of
{
	public interface IMouseOfTargetMethodChainNode<out TCurrentMethodChainNode, out TNextMethodChainNode> : IDomElementOfTargetMethodChainNode<TCurrentMethodChainNode, TNextMethodChainNode> 
		where TNextMethodChainNode : IBaseMethodChainNode
		where TCurrentMethodChainNode : IBaseMethodChainNode
	{
		TNextMethodChainNode Of(int x, int y);
		TNextMethodChainNode Of(IDomElement element, int relativeX, int relativeY);
	}
}
