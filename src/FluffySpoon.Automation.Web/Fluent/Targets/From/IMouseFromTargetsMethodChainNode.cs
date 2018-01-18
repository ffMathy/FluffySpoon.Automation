using FluffySpoon.Automation.Web.Dom;
using System.Collections.Generic;

namespace FluffySpoon.Automation.Web.Fluent.Targets.From
{
	public interface IMouseFromTargetsMethodChainNode<TNextMethodChainNode> : 
		IMouseFromTargetMethodChainNode<TNextMethodChainNode>, 
		IDomElementFromTargetsMethodChainNode<TNextMethodChainNode> 
		where TNextMethodChainNode : IBaseMethodChainNode
	{
		TNextMethodChainNode From(IReadOnlyCollection<IDomElement> elements, int relativeX, int relativeY);
	}
}
