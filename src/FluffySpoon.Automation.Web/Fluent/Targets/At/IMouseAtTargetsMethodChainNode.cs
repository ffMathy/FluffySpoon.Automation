using FluffySpoon.Automation.Web.Dom;
using System.Collections.Generic;

namespace FluffySpoon.Automation.Web.Fluent.Targets.At
{
	public interface IMouseAtTargetsMethodChainNode<out TCurrentMethodChainNode, out TNextMethodChainNode> : 
		IMouseAtTargetMethodChainNode<TCurrentMethodChainNode, TNextMethodChainNode>, 
		IDomElementAtTargetsMethodChainNode<TCurrentMethodChainNode, TNextMethodChainNode> 
		where TNextMethodChainNode : IBaseMethodChainNode
		where TCurrentMethodChainNode : IBaseMethodChainNode
	{
		TNextMethodChainNode At(IReadOnlyList<IDomElement> elements, int relativeX, int relativeY);
	}
}
