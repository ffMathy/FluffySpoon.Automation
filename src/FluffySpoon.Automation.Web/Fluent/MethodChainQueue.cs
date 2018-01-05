using System.Collections.Generic;
using System.Threading.Tasks;

namespace FluffySpoon.Automation.Web.Fluent
{
    class MethodChainQueue : IMethodChainQueue
    {
        private readonly Queue<IBaseMethodChainNode> _pendingNodesToRun;

        public MethodChainQueue()
        {
            _pendingNodesToRun = new Queue<IBaseMethodChainNode>();
        }

        public async Task RunAllAsync(IWebAutomationTechnology technology)
        {
            while(_pendingNodesToRun.Count > 0)
                await RunNextAsync(technology);
        }

        public async Task RunNextAsync(IWebAutomationTechnology technology)
        {
            await _pendingNodesToRun.Dequeue();
        }

        public TMethodChainNode Enqueue<TMethodChainNode>(TMethodChainNode node) where TMethodChainNode : IBaseMethodChainNode
        {
            _pendingNodesToRun.Enqueue(node);
            return node;
        }
    }
}