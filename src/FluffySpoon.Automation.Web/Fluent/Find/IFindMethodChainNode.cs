using FluffySpoon.Automation.Web.Dom;
using System.Collections.Generic;

namespace FluffySpoon.Automation.Web.Fluent.Find
{
	public interface IFindMethodChainNode : IBaseMethodChainNode, IAwaitable<IReadOnlyList<IDomElement>>
	{
	}
}