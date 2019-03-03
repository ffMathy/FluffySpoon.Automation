using System.Collections.Generic;
using FluffySpoon.Automation.Web.Dom;
using FluffySpoon.Automation.Web.Fluent.Root;

namespace FluffySpoon.Automation.Web.Fluent.Hover
{
	public interface IHoverOnTargetMethodChainNode : IMethodChainRoot, IBaseMethodChainNode, IAwaitable<IReadOnlyList<IDomElement>>
	{
	}
}