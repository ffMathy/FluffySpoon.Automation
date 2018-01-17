using System.Threading.Tasks;

namespace FluffySpoon.Automation.Web.Fluent
{
    public interface IBaseMethodChainNode: IAwaitable
    {
		IMethodChainContext MethodChainContext { set; }

		Task ExecuteAsync(IWebAutomationFrameworkInstance framework);
    }
}