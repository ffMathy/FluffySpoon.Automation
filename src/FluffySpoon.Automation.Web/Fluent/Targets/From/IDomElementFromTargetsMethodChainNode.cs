using FluffySpoon.Automation.Web.Dom;
using System.Collections.Generic;

namespace FluffySpoon.Automation.Web.Fluent.Targets.From
{
	public interface IDomElementFromTargetsMethodChainNode<out TCurrentMethodChainNode, out TNextMethodChainNode> : IDomElementFromTargetMethodChainNode<TCurrentMethodChainNode, TNextMethodChainNode> 
		where TNextMethodChainNode : IBaseMethodChainNode
		where TCurrentMethodChainNode : IBaseMethodChainNode
    {
		TNextMethodChainNode From(IReadOnlyList<IDomElement> elements);
	}
}
