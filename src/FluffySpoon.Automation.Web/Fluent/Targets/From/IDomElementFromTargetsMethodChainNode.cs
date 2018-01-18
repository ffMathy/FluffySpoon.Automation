using FluffySpoon.Automation.Web.Dom;
using System.Collections.Generic;

namespace FluffySpoon.Automation.Web.Fluent.Targets.From
{
	public interface IDomElementFromTargetsMethodChainNode<TNextMethodChainNode> : IDomElementFromTargetMethodChainNode<TNextMethodChainNode> where TNextMethodChainNode : IBaseMethodChainNode
    {
		TNextMethodChainNode From(IReadOnlyCollection<IDomElement> elements);
	}
}
