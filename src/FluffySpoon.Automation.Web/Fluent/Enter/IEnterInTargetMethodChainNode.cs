using FluffySpoon.Automation.Web.Dom;
using FluffySpoon.Automation.Web.Fluent.Root;
using System.Collections.Generic;

namespace FluffySpoon.Automation.Web.Fluent.Enter
{
	public interface IEnterInTargetMethodChainNode : 
		IBaseMethodChainNode, 
		IMethodChainRoot,
		IAwaitable
	{
	}
}