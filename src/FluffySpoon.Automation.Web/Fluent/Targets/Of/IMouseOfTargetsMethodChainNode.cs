using FluffySpoon.Automation.Web.Dom;
using System.Collections.Generic;

namespace FluffySpoon.Automation.Web.Fluent.Targets.Of
{
	public interface IMouseOfTargetsMethodChainNode<TNextMethodChainNode> : 
		IMouseOfTargetMethodChainNode<TNextMethodChainNode>, 
		IDomElementOfTargetsMethodChainNode<TNextMethodChainNode> 
		where TNextMethodChainNode : IBaseMethodChainNode
	{
		TNextMethodChainNode Of(IReadOnlyCollection<IDomElement> elements, int relativeX, int relativeY);
	}
}
