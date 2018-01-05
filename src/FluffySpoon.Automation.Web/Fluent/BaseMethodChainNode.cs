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

        public async Task ExecuteAsync(IWebAutomationFrameworkInstance framework)
        {
            await OnExecuteAsync(framework);
        }

        protected virtual Task OnExecuteAsync(IWebAutomationFrameworkInstance framework)
        {
            return Task.CompletedTask;
        }
    }
}