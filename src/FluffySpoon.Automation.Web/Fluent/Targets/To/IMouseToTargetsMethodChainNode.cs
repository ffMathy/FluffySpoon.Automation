using FluffySpoon.Automation.Web.Dom;
using System.Collections.Generic;

namespace FluffySpoon.Automation.Web.Fluent.Targets.To
{
	public interface IMouseToTargetsMethodChainNode<out TCurrentMethodChainNode, out TNextMethodChainNode> : 
		IMouseToTargetMethodChainNode<TCurrentMethodChainNode, TNextMethodChainNode>, 
		IDomElementToTargetsMethodChainNode<TCurrentMethodChainNode, TNextMethodChainNode> 
		where TNextMethodChainNode : IBaseMethodChainNode
		where TCurrentMethodChainNode : IBaseMethodChainNode
	{
		TNextMethodChainNode To(IReadOnlyList<IDomElement> elements, int relativeX, int relativeY);
	}
}
