using FluffySpoon.Automation.Web.Dom;
using System.Collections.Generic;

namespace FluffySpoon.Automation.Web.Fluent.Targets.To
{
	public interface IDomElementToTargetsMethodChainNode<out TCurrentMethodChainNode, out TNextMethodChainNode> : IDomElementToTargetMethodChainNode<TCurrentMethodChainNode, TNextMethodChainNode> 
		where TNextMethodChainNode : IBaseMethodChainNode
		where TCurrentMethodChainNode : IBaseMethodChainNode
    {
		TNextMethodChainNode To(IReadOnlyList<IDomElement> elements);
	}
}
