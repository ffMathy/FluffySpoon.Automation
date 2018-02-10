using System;
using FluffySpoon.Automation.Web.Fluent.Context;
using System.Threading.Tasks;

namespace FluffySpoon.Automation.Web.Fluent
{
	public interface IBaseMethodChainNode
	{
		//TODO: find a way to hide many of these from public interface.

		IMethodChainContext MethodChainContext { set; }
	
		Task ExecuteAsync(IWebAutomationFrameworkInstance framework);

		void SetParent(IBaseMethodChainNode parent);

		IBaseMethodChainNode Clone();
	}
}