using FluffySpoon.Automation.Web.Dom;
using System.Collections.Generic;

namespace FluffySpoon.Automation.Web.Fluent.Targets.From
{
	public interface IMouseFromTargetsMethodChainNode<out TCurrentMethodChainNode, out TNextMethodChainNode> : 
		IMouseFromTargetMethodChainNode<TCurrentMethodChainNode, TNextMethodChainNode>, 
		IDomElementFromTargetsMethodChainNode<TCurrentMethodChainNode, TNextMethodChainNode> 
		where TNextMethodChainNode : IBaseMethodChainNode<TCurrentMethodChainNode>
		where TCurrentMethodChainNode : IBaseMethodChainNode
	{
		TNextMethodChainNode From(IReadOnlyList<IDomElement> elements, int relativeX, int relativeY);
	}
}
