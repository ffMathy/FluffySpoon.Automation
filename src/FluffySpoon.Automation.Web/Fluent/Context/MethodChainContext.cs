using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Threading.Tasks;

namespace FluffySpoon.Automation.Web.Fluent.Context
{
    class MethodChainContext : IMethodChainContext
    {
        private readonly IEnumerable<IWebAutomationFrameworkInstance> _frameworks;
        private readonly Queue<IBaseMethodChainNode> _pendingNodesToRun;

		private readonly SemaphoreSlim _runNextSemaphore;

		public MethodChainContext(
            IEnumerable<IWebAutomationFrameworkInstance> frameworks)
        {
            _pendingNodesToRun = new Queue<IBaseMethodChainNode>();
			_runNextSemaphore = new SemaphoreSlim(1);
            
            _frameworks = frameworks;
        }

        public async Task RunAllAsync()
        {
            while(_pendingNodesToRun.Count > 0)
                await RunNextAsync();
        }

        public async Task RunNextAsync()
        {
			await _runNextSemaphore.WaitAsync();

			if (_pendingNodesToRun.Count > 0)
			{
				var next = _pendingNodesToRun.Dequeue();
				await Task.WhenAll(_frameworks.Select(next.ExecuteAsync));
			}

			_runNextSemaphore.Release(1);
        }

        public TMethodChainNode Enqueue<TMethodChainNode>(TMethodChainNode node) where TMethodChainNode : IBaseMethodChainNode
        {
			node.MethodChainContext = this;
            _pendingNodesToRun.Enqueue(node);

			Task.Factory.StartNew(RunAllAsync);

            return node;
        }

        public TaskAwaiter GetAwaiter()
        {
            return RunAllAsync().GetAwaiter();
        }
    }
}