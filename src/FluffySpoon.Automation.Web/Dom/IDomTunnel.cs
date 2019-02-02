using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace FluffySpoon.Automation.Web.Dom
{
	public interface IDomTunnel
	{
		Task<IReadOnlyList<IDomElement>> GetDomElementsFromJavaScriptCode(IWebAutomationFrameworkInstance automationFrameworkInstance, int methodChainOffset, string scriptToExecute);
		Task<IReadOnlyList<IDomElement>> GetDomElementsFromSelector(IWebAutomationFrameworkInstance automationFrameworkInstance, int methodChainOffset, string selector);
	}
}
