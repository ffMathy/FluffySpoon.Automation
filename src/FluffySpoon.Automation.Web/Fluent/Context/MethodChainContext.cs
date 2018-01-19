using System;
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

        private readonly LinkedList<IBaseMethodChainNode> _allNodes;
        private readonly Queue<IBaseMethodChainNode> _pendingNodesToRun;

		private readonly SemaphoreSlim _semaphore;

		public MethodChainContext(
            IEnumerable<IWebAutomationFrameworkInstance> frameworks)
        {
            _allNodes = new LinkedList<IBaseMethodChainNode>();
            _pendingNodesToRun = new Queue<IBaseMethodChainNode>();
			_semaphore = new SemaphoreSlim(1);
            
            _frameworks = frameworks;
        }

        public async Task RunAllAsync()
        {
            while(_allNodes.Count > 0)
                await RunNextAsync();
        }

        public async Task RunNextAsync()
        {
			await _semaphore.WaitAsync();

			if (_pendingNodesToRun.Count > 0)
			{
				var next = _pendingNodesToRun.Dequeue();
				_allNodes.RemoveFirst();

				await Task.WhenAll(_frameworks.Select(next.ExecuteAsync));
			}

			_semaphore.Release(1);
        }

        public TMethodChainNode Enqueue<TMethodChainNode>(TMethodChainNode node) where TMethodChainNode : IBaseMethodChainNode
        {
			_semaphore.Wait();

			node.MethodChainContext = this;

            var linkedListNode = _allNodes.AddLast(node);
			if(node is IBaseMethodChainNode<IBaseMethodChainNode> nodeWithParent)
				nodeWithParent.Parent = linkedListNode?.Previous?.Value;

			_pendingNodesToRun.Enqueue(node);

			_semaphore.Release(1);

			Task.Factory.StartNew(RunAllAsync);

            return node;
        }

        public TaskAwaiter GetAwaiter()
        {
            return RunAllAsync().GetAwaiter();
        }
    }
}