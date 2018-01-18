using FluffySpoon.Automation.Web.Dom;
using System.Collections.Generic;

namespace FluffySpoon.Automation.Web.Fluent.Targets.On
{
	public interface IDomElementOnTargetsMethodChainNode<TNextMethodChainNode> : IDomElementOnTargetMethodChainNode<TNextMethodChainNode> where TNextMethodChainNode : IBaseMethodChainNode
    {
		TNextMethodChainNode On(IReadOnlyCollection<IDomElement> elements);
	}
}
