using FluffySpoon.Automation.Web.Dom;
using System.Collections.Generic;

namespace FluffySpoon.Automation.Web.Fluent.Targets.At
{
	public interface IDomElementAtTargetsMethodChainNode<out TCurrentMethodChainNode, out TNextMethodChainNode> : IDomElementAtTargetMethodChainNode<TCurrentMethodChainNode, TNextMethodChainNode> 
		where TNextMethodChainNode : IBaseMethodChainNode
		where TCurrentMethodChainNode : IBaseMethodChainNode
    {
		TNextMethodChainNode At(IReadOnlyList<IDomElement> elements);
	}
}
