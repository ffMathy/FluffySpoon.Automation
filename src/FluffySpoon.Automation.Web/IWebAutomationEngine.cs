using System.Threading.Tasks;
using FluffySpoon.Automation.Web.Fluent.Root;

namespace FluffySpoon.Automation.Web
{
	public interface IWebAutomationEngine: IMethodChainRoot, IAwaitable
    {
		Task InitializeAsync();
    }
}