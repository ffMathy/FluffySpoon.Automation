using FluffySpoon.Automation.Web.Dom;
using FluffySpoon.Automation.Web.Fluent.Root;
using System.Collections.Generic;

namespace FluffySpoon.Automation.Web.Fluent.Select
{
	public interface ISelectByMethodChainNode: IBaseMethodChainNode
	{
		IMethodChainRoot From(string selector);
		IMethodChainRoot From(IDomElement element);
		IMethodChainRoot From(IReadOnlyCollection<IDomElement> elements);
	}
}