using System.Collections.Generic;
using FluffySpoon.Automation.Web.Dom;
using FluffySpoon.Automation.Web.Fluent.Root;

namespace FluffySpoon.Automation.Web.Fluent.DoubleClick
{
	public interface IDoubleClickOnTargetsMethodChainNode : IMethodChainRoot, IBaseMethodChainNode, IAwaitable<IReadOnlyList<IDomElement>>
	{
	}
}