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
		private static volatile int MethodChainOffset;

		private readonly IEnumerable<IWebAutomationFrameworkInstance> _frameworks;

		private readonly LinkedList<IBaseMethodChainNode> _allNodes;
		private readonly Queue<IBaseMethodChainNode> _pendingNodesToRun;

		private readonly SemaphoreSlim _semaphore;

		private readonly int _methodChainOffset;

		public MethodChainContext(
			IEnumerable<IWebAutomationFrameworkInstance> frameworks)
		{
			_allNodes = new LinkedList<IBaseMethodChainNode>();
			_pendingNodesToRun = new Queue<IBaseMethodChainNode>();
			_semaphore = new SemaphoreSlim(1);

			_frameworks = frameworks;

			_methodChainOffset = Interlocked.Increment(ref MethodChainOffset);
		}

		public async Task RunAllAsync()
		{
			while (_pendingNodesToRun.Count > 0)
				await RunNextAsync();
		}

		public async Task RunNextAsync()
		{
			try
			{
				await _semaphore.WaitAsync();

				if (_pendingNodesToRun.Count > 0)
				{
					var next = _pendingNodesToRun.Dequeue();
					Log("Executing: " + next.GetType().Name);

					await Task.WhenAll(_frameworks.Select(next.ExecuteAsync));
				}
			}
			finally
			{
				_semaphore.Release(1);
			}
		}

		public TMethodChainNode Enqueue<TMethodChainNode>(TMethodChainNode node) where TMethodChainNode : class, IBaseMethodChainNode
		{
			try
			{
				_semaphore.Wait();

				if (node == _allNodes.Last?.Value)
					return node;

				node.MethodChainContext = this;

				var linkedListNode = _allNodes.AddLast(node);
				var parentNode = linkedListNode?.Previous?.Value;
				if (parentNode != null)
					node.SetParent(parentNode);

				_allNodes.AddLast(node);
				_pendingNodesToRun.Enqueue(node);

				Log("Queued: " + node.GetType().Name);
			}
			finally
			{
				_semaphore.Release(1);
			}

			Task.Factory.StartNew(RunAllAsync);

			return node;
		}

		public TaskAwaiter GetAwaiter()
		{
			return RunAllAsync().GetAwaiter();
		}

		private void Log(string message)
		{
			Console.WriteLine("[#" + _methodChainOffset + "] " + message);
		}
	}
}