using System.Collections.Generic;
using FluffySpoon.Automation.Web.Dom;
using FluffySpoon.Automation.Web.Fluent.Expect.Root;

namespace FluffySpoon.Automation.Web.Fluent.Expect.Count
{
	public interface IExpectCountOfTargetsMethodChainNode: IExpectMethodChainRoot, IBaseExpectMethodChainNode, IAwaitable<IReadOnlyList<IDomElement>>
	{
	}
}