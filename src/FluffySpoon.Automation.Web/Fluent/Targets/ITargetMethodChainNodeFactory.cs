using System.Collections.Generic;
using FluffySpoon.Automation.Web.Dom;

namespace FluffySpoon.Automation.Web.Fluent.Targets
{
	public interface ITargetMethodChainNodeFactory<out TNextMethodChainNode> where TNextMethodChainNode : IBaseMethodChainNode
	{
		TNextMethodChainNode Create(IReadOnlyList<IDomElement> elements);
	}
}