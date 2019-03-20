using FluffySpoon.Automation.Web.Dom;
using FluffySpoon.Automation.Web.Fluent.Expect.Root;
using System.Collections.Generic;

namespace FluffySpoon.Automation.Web.Fluent.Expect.Value
{
	public interface IExpectValueOfTargetsMethodChainNode: IExpectMethodChainRoot, IBaseExpectMethodChainNode, IAwaitable<IReadOnlyList<IDomElement>>
	{
	}
}