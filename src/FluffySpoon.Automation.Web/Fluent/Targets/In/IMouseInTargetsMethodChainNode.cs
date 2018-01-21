using FluffySpoon.Automation.Web.Dom;
using System.Collections.Generic;

namespace FluffySpoon.Automation.Web.Fluent.Targets.In
{
	public interface IMouseInTargetsMethodChainNode<out TCurrentMethodChainNode, out TNextMethodChainNode> : 
		IMouseInTargetMethodChainNode<TCurrentMethodChainNode, TNextMethodChainNode>, 
		IDomElementInTargetsMethodChainNode<TCurrentMethodChainNode, TNextMethodChainNode> 
		where TNextMethodChainNode : IBaseMethodChainNode
		where TCurrentMethodChainNode : IBaseMethodChainNode
	{
		TNextMethodChainNode In(IReadOnlyList<IDomElement> elements, int relativeX, int relativeY);
	}
}
