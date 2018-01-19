using FluffySpoon.Automation.Web.Dom;
using System.Collections.Generic;

namespace FluffySpoon.Automation.Web.Fluent.Targets.On
{
	public interface IDomElementOnTargetsMethodChainNode<out TCurrentMethodChainNode, out TNextMethodChainNode> : IDomElementOnTargetMethodChainNode<TCurrentMethodChainNode, TNextMethodChainNode> 
		where TNextMethodChainNode : IBaseMethodChainNode<TCurrentMethodChainNode>
		where TCurrentMethodChainNode : IBaseMethodChainNode
    {
		TNextMethodChainNode On(IReadOnlyList<IDomElement> elements);
	}
}
