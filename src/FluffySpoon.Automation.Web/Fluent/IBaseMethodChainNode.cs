using System.Threading.Tasks;

namespace FluffySpoon.Automation.Web.Fluent
{
    public interface IBaseMethodChainNode: IAwaitable
    {
        Task ExecuteAsync(IWebAutomationFrameworkInstance framework);
    }
}