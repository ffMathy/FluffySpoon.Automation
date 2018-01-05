using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;

namespace FluffySpoon.Automation.Web.Fluent
{
    class MethodChainContext : IMethodChainContext
    {
        private readonly IEnumerable<IWebAutomationTechnology> _technologies;

        private readonly Queue<IBaseMethodChainNode> _pendingNodesToRun;

        public MethodChainContext(
            IEnumerable<IWebAutomationTechnology> technologies)
        {
            _pendingNodesToRun = new Queue<IBaseMethodChainNode>();
            
            _technologies = technologies;
        }

        public async Task RunAllAsync()
        {
            while(_pendingNodesToRun.Count > 0)
                await RunNextAsync();
        }

        public async Task RunNextAsync()
        {
            var next = _pendingNodesToRun.Dequeue();
            await Task.WhenAll(_technologies.Select(next.ExecuteAsync));
        }

        public TMethodChainNode Enqueue<TMethodChainNode>(TMethodChainNode node) where TMethodChainNode : IBaseMethodChainNode
        {
            _pendingNodesToRun.Enqueue(node);
            return node;
        }

        public TaskAwaiter GetAwaiter()
        {
            return RunAllAsync().GetAwaiter();
        }
    }
}