using System.Threading.Tasks;
using FluffySpoon.Automation.Web.Async;

namespace FluffySpoon.Automation.Web.Fluent
{
    public interface IBaseMethodChainNode: IAwaitable
    {
        Task ExecuteAsync(IWebAutomationTechnology technology);
    }
}