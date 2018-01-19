using FluffySpoon.Automation.Web.Dom;
using System.Collections.Generic;

namespace FluffySpoon.Automation.Web.Fluent.Targets.From
{
	public interface IMouseFromTargetsMethodChainNode<out TNextMethodChainNode> : 
		IMouseFromTargetMethodChainNode<TNextMethodChainNode>, 
		IDomElementFromTargetsMethodChainNode<TNextMethodChainNode> 
		where TNextMethodChainNode : IBaseMethodChainNode
	{
		TNextMethodChainNode From(IReadOnlyList<IDomElement> elements, int relativeX, int relativeY);
	}
}
