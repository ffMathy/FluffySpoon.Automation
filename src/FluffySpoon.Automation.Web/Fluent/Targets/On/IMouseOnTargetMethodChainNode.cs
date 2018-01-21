using FluffySpoon.Automation.Web.Dom;

namespace FluffySpoon.Automation.Web.Fluent.Targets.On
{
	public interface IMouseOnTargetMethodChainNode<out TCurrentMethodChainNode, out TNextMethodChainNode> : IDomElementOnTargetMethodChainNode<TCurrentMethodChainNode, TNextMethodChainNode> 
		where TNextMethodChainNode : IBaseMethodChainNode
		where TCurrentMethodChainNode : IBaseMethodChainNode
	{
		TNextMethodChainNode On(int x, int y);
		TNextMethodChainNode On(IDomElement element, int relativeX, int relativeY);
	}
}
