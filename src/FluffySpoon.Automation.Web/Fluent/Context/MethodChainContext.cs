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

		private Exception _lastEncounteredException;

		private readonly LinkedList<IBaseMethodChainNode> _allNodes;
		private readonly Queue<IBaseMethodChainNode> _pendingNodesToRun;

		private readonly SemaphoreSlim _semaphore;

		private readonly int _methodChainOffset;

		public IEnumerable<IWebAutomationFrameworkInstance> Frameworks { get; private set; }

		public MethodChainContext(
			IEnumerable<IWebAutomationFrameworkInstance> frameworks)
		{
			_allNodes = new LinkedList<IBaseMethodChainNode>();
			_pendingNodesToRun = new Queue<IBaseMethodChainNode>();
			_semaphore = new SemaphoreSlim(1);

			Frameworks = frameworks;

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

				try
				{

					if (_pendingNodesToRun.Count > 0)
					{
						var next = _pendingNodesToRun.Dequeue();
						Log("Executing: " + next.GetType().Name);

						await Task.WhenAll(Frameworks.Select(next.ExecuteAsync));
					}

				}
				catch (AggregateException ex)
				{
					_lastEncounteredException =
						ex.InnerExceptions.Count == 1 ? ex.InnerExceptions.Single() : ex;
				}
				catch (Exception ex)
				{
					_lastEncounteredException = ex;
				}

				ThrowExceptionIfPresent();
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
				ThrowExceptionIfPresent();

				var isQueueEmpty = _pendingNodesToRun.Count == 0;

				if (node == _allNodes.Last?.Value)
					return node;

				node.MethodChainContext = this;

				var linkedListNode = _allNodes.AddLast(node);
				var parentNode = linkedListNode?.Previous?.Value;
				if (parentNode != null)
					node.SetParent(parentNode);

				_pendingNodesToRun.Enqueue(node);

				Log("Queued: " + node.GetType().Name);

				if (isQueueEmpty)
					Task.Factory
						.StartNew(RunAllAsync)
						;
			}
			finally
			{
				_semaphore.Release(1);
			}

			return node;
		}

		public TaskAwaiter GetAwaiter()
		{
			ThrowExceptionIfPresent();
			return RunAllAsync().GetAwaiter();
		}

		private void ThrowExceptionIfPresent()
		{
			if (_lastEncounteredException != null)
				throw new ApplicationException(
					"An error occured while performing one of the automation operations.", 
					_lastEncounteredException);
		}

		private void Log(string message)
		{
			Console.WriteLine("[#" + _methodChainOffset + "] " + message);
		}

		public void ResetLastError()
		{
			_lastEncounteredException = null;
		}
	}
}