using System.Runtime.CompilerServices;
using System.Threading.Tasks;

namespace FluffySpoon.Automation.Web.Fluent
{
    abstract class BaseMethodChainNode : IBaseMethodChainNode
    {
        protected readonly IMethodChainContext MethodChainContext;

        public BaseMethodChainNode(
            IMethodChainContext methodChainContext)
        {
            MethodChainContext = methodChainContext;
        }

        public TaskAwaiter GetAwaiter()
        {
            return MethodChainContext.GetAwaiter();
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