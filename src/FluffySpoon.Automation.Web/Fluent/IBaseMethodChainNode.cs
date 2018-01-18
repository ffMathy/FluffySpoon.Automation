using FluffySpoon.Automation.Web.Fluent.Context;
using System.Threading.Tasks;

namespace FluffySpoon.Automation.Web.Fluent
{
    public interface IBaseMethodChainNode
    {
		IMethodChainContext MethodChainContext { set; }

		Task ExecuteAsync(IWebAutomationFrameworkInstance framework);
    }
}