using System.Collections.Generic;
using System.Threading.Tasks;

namespace FluffySpoon.Automation.Web.Dom
{
	public interface IDomTunnel
	{
		Task<IReadOnlyList<IDomElement>> FindDomElementsByCssSelectorsAsync(IWebAutomationFrameworkInstance automationFrameworkInstance, int methodChainOffset, string[] selectors);
		Task<IReadOnlyList<IDomElement>> GetDomElementsFromJavaScriptCode(IWebAutomationFrameworkInstance automationFrameworkInstance, int methodChainOffset, string scriptToExecute);
		Task<IReadOnlyList<IDomElement>> GetDomElementsFromSelector(IWebAutomationFrameworkInstance automationFrameworkInstance, int methodChainOffset, string selector);
	}
}
