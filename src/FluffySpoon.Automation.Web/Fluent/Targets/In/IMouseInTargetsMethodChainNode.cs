using FluffySpoon.Automation.Web.Dom;
using System.Collections.Generic;

namespace FluffySpoon.Automation.Web.Fluent.Targets.In
{
	public interface IMouseInTargetsMethodChainNode<TNextMethodChainNode> : 
		IMouseInTargetMethodChainNode<TNextMethodChainNode>, 
		IDomElementInTargetsMethodChainNode<TNextMethodChainNode> 
		where TNextMethodChainNode : IBaseMethodChainNode
	{
		TNextMethodChainNode In(IReadOnlyCollection<IDomElement> elements, int relativeX, int relativeY);
	}
}
