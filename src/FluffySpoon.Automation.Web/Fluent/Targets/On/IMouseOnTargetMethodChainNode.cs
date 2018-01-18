using FluffySpoon.Automation.Web.Dom;

namespace FluffySpoon.Automation.Web.Fluent.Targets.On
{
	public interface IMouseOnTargetMethodChainNode<TNextMethodChainNode> : IDomElementOnTargetMethodChainNode<TNextMethodChainNode> where TNextMethodChainNode : IBaseMethodChainNode
	{
		TNextMethodChainNode On(int x, int y);
		TNextMethodChainNode On(IDomElement element, int relativeX, int relativeY);
	}
}
