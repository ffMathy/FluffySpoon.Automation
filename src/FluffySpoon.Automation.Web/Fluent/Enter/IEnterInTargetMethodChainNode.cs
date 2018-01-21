using FluffySpoon.Automation.Web.Fluent.Root;
using FluffySpoon.Automation.Web.Fluent.Targets;

namespace FluffySpoon.Automation.Web.Fluent.Enter
{
	public interface IEnterInTargetMethodChainNode : 
		IBaseMethodChainNode, 
		IAwaitable, 
		IMethodChainRoot
	{
	}
}