using FluffySpoon.Automation.Web.Dom;
using System.Collections.Generic;

namespace FluffySpoon.Automation.Web.Fluent.Targets.In
{
	public interface IDomElementInTargetsMethodChainNode<out TCurrentMethodChainNode, out TNextMethodChainNode> : IDomElementInTargetMethodChainNode<TCurrentMethodChainNode, TNextMethodChainNode> 
		where TNextMethodChainNode : IBaseMethodChainNode
		where TCurrentMethodChainNode : IBaseMethodChainNode
		
    {
		TNextMethodChainNode In(IReadOnlyList<IDomElement> elements);
	}
}
