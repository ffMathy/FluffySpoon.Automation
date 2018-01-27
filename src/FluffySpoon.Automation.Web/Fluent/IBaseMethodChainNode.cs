using System.Collections.Generic;
using FluffySpoon.Automation.Web.Fluent.Context;
using System.Threading.Tasks;
using FluffySpoon.Automation.Web.Dom;

namespace FluffySpoon.Automation.Web.Fluent
{
	public interface IBaseMethodChainNode
	{
		IMethodChainContext MethodChainContext { set; }
	
		Task ExecuteAsync(IWebAutomationFrameworkInstance framework);

		//TODO: find a way to hide this from public interface.
		void SetParent(IBaseMethodChainNode parent);
	}
}