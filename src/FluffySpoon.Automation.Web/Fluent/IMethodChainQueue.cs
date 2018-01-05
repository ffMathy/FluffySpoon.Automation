using System.Threading.Tasks;

namespace FluffySpoon.Automation.Web.Fluent
{
    public interface IMethodChainQueue
    {
        Task RunAllAsync(IWebAutomationTechnology technology);
        Task RunNextAsync(IWebAutomationTechnology technology);

        TMethodChainNode Enqueue<TMethodChainNode>(TMethodChainNode node) where TMethodChainNode : IBaseMethodChainNode;
    }
}