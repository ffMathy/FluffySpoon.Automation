using FluffySpoon.Automation.Web.Dom;
using FluffySpoon.Automation.Web.Fluent.Root;
using System.Collections.Generic;

namespace FluffySpoon.Automation.Web.Fluent.Focus
{
	public interface IFocusOnTargetMethodChainNode : IMethodChainRoot, IBaseMethodChainNode, IAwaitable<IReadOnlyList<IDomElement>>
	{
	}
}