using FluffySpoon.Automation.Web.Dom;
using System.Collections.Generic;

namespace FluffySpoon.Automation.Web.Fluent.Targets.In
{
	public interface IDomElementInTargetsMethodChainNode<TNextMethodChainNode> : IDomElementInTargetMethodChainNode<TNextMethodChainNode> where TNextMethodChainNode : IBaseMethodChainNode
    {
		TNextMethodChainNode In(IReadOnlyCollection<IDomElement> elements);
	}
}
