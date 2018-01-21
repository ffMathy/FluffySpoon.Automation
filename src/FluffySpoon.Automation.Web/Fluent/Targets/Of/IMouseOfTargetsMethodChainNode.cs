using FluffySpoon.Automation.Web.Dom;
using System.Collections.Generic;

namespace FluffySpoon.Automation.Web.Fluent.Targets.Of
{
	public interface IMouseOfTargetsMethodChainNode<out TCurrentMethodChainNode, out TNextMethodChainNode> : 
		IMouseOfTargetMethodChainNode<TCurrentMethodChainNode, TNextMethodChainNode>, 
		IDomElementOfTargetsMethodChainNode<TCurrentMethodChainNode, TNextMethodChainNode> 
		where TNextMethodChainNode : IBaseMethodChainNode
		where TCurrentMethodChainNode : IBaseMethodChainNode
	{
		TNextMethodChainNode Of(IReadOnlyList<IDomElement> elements, int relativeX, int relativeY);
	}
}
