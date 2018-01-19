using FluffySpoon.Automation.Web.Dom;
using System.Collections.Generic;

namespace FluffySpoon.Automation.Web.Fluent.Targets.Of
{
	public interface IDomElementOfTargetsMethodChainNode<out TCurrentMethodChainNode, out TNextMethodChainNode> : IDomElementOfTargetMethodChainNode<TCurrentMethodChainNode, TNextMethodChainNode> 
		where TNextMethodChainNode : IBaseMethodChainNode<TCurrentMethodChainNode>
		where TCurrentMethodChainNode : IBaseMethodChainNode
    {
		TNextMethodChainNode Of(IReadOnlyList<IDomElement> elements);
	}
}
