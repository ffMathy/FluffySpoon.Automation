using FluffySpoon.Automation.Web.Dom;
using System.Collections.Generic;

namespace FluffySpoon.Automation.Web.Fluent.Targets.On
{
	public interface IMouseOnTargetsMethodChainNode<out TCurrentMethodChainNode, out TNextMethodChainNode> : 
		IMouseOnTargetMethodChainNode<TCurrentMethodChainNode, TNextMethodChainNode>, 
		IDomElementOnTargetsMethodChainNode<TCurrentMethodChainNode, TNextMethodChainNode> 
		where TNextMethodChainNode : IBaseMethodChainNode<TCurrentMethodChainNode>
		where TCurrentMethodChainNode : IBaseMethodChainNode
	{
		TNextMethodChainNode On(IReadOnlyList<IDomElement> elements, int relativeX, int relativeY);
	}
}
