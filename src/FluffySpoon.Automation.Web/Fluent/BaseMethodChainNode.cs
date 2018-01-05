using System.Runtime.CompilerServices;
using System.Threading.Tasks;

namespace FluffySpoon.Automation.Web.Fluent
{
    abstract class BaseMethodChainNode : IBaseMethodChainNode
    {
        protected readonly IMethodChainQueue MethodChainQueue;

        public BaseMethodChainNode(
            IMethodChainQueue methodChainQueue)
        {
            MethodChainQueue = methodChainQueue;
        }

        public TaskAwaiter GetAwaiter()
        {
            return MethodChainQueue.RunAllAsync().GetAwaiter();
        }

        public async Task ExecuteAsync(IWebAutomationTechnology technology)
        {
            await OnExecuteAsync(technology);
        }

        protected virtual Task OnExecuteAsync(IWebAutomationTechnology technology)
        {
            return Task.CompletedTask;
        }
    }
}