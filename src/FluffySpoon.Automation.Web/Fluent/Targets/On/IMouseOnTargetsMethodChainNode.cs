using FluffySpoon.Automation.Web.Dom;
using System.Collections.Generic;

namespace FluffySpoon.Automation.Web.Fluent.Targets.On
{
	public interface IMouseOnTargetsMethodChainNode<TNextMethodChainNode> : 
		IMouseOnTargetMethodChainNode<TNextMethodChainNode>, 
		IDomElementOnTargetsMethodChainNode<TNextMethodChainNode> 
		where TNextMethodChainNode : IBaseMethodChainNode
	{
		TNextMethodChainNode On(IReadOnlyCollection<IDomElement> elements, int relativeX, int relativeY);
	}
}
