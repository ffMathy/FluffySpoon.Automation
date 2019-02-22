using System.Collections.Generic;
using FluffySpoon.Automation.Web.Dom;
using FluffySpoon.Automation.Web.Fluent.Root;

namespace FluffySpoon.Automation.Web.Fluent.RightClick
{
	public interface IRightClickOnTargetsMethodChainNode : IMethodChainRoot, IBaseMethodChainNode, IAwaitable<IReadOnlyList<IDomElement>>
	{
	}
}