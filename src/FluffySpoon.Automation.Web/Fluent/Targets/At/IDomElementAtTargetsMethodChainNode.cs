using FluffySpoon.Automation.Web.Dom;
using System.Collections.Generic;

namespace FluffySpoon.Automation.Web.Fluent.Targets.At
{
	public interface IDomElementAtTargetsMethodChainNode<TNextMethodChainNode> : IDomElementAtTargetMethodChainNode<TNextMethodChainNode> where TNextMethodChainNode : IBaseMethodChainNode
    {
		TNextMethodChainNode At(IReadOnlyCollection<IDomElement> elements);
	}
}
