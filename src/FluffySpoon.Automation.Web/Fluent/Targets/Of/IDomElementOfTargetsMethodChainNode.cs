using FluffySpoon.Automation.Web.Dom;
using System.Collections.Generic;

namespace FluffySpoon.Automation.Web.Fluent.Targets.Of
{
	public interface IDomElementOfTargetsMethodChainNode<TNextMethodChainNode> : IDomElementOfTargetMethodChainNode<TNextMethodChainNode> where TNextMethodChainNode : IBaseMethodChainNode
    {
		TNextMethodChainNode Of(IReadOnlyCollection<IDomElement> elements);
	}
}
