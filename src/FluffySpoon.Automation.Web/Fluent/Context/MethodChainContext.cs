using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Threading.Tasks;

namespace FluffySpoon.Automation.Web.Fluent.Context
{
	class MethodChainContext : IMethodChainContext
	{
		private class MethodChain
		{
			public LinkedList<IBaseMethodChainNode> AllNodes { get; }
			public Queue<IBaseMethodChainNode> PendingNodesToRun { get; }

			public MethodChain()
			{
				AllNodes = new LinkedList<IBaseMethodChainNode>();
				PendingNodesToRun = new Queue<IBaseMethodChainNode>();
			}
		}

		private static volatile int _globalMethodChainOffset;
		private readonly int _methodChainOffset;

		private int _nodeCount;

		private Exception _lastEncounteredException;

		private readonly SemaphoreSlim _semaphore;

		private readonly IDictionary<IWebAutomationFrameworkInstance, MethodChain> _userAgentMethodChainQueue;

		public IEnumerable<IWebAutomationFrameworkInstance> Frameworks { get; private set; }

		public MethodChainContext(
			IEnumerable<IWebAutomationFrameworkInstance> frameworks)
		{
			_userAgentMethodChainQueue = new Dictionary<IWebAutomationFrameworkInstance, MethodChain>();

			_semaphore = new SemaphoreSlim(1);

			Frameworks = frameworks;

			_methodChainOffset = Interlocked.Increment(ref _globalMethodChainOffset);

			foreach(var framework in Frameworks)
			{
				_userAgentMethodChainQueue.Add(framework, new MethodChain());
			}
		}

		public async Task RunAllAsync()
		{
			while (_nodeCount > 0)
				await RunNextAsync();
		}

		public async Task RunNextAsync()
		{
			try
			{
				await _semaphore.WaitAsync();

				try
				{
					if (_nodeCount > 0)
					{
						_nodeCount--;

						var tasks = new List<Task>();
						foreach (var framework in Frameworks)
						{
							var methodChainQueue = _userAgentMethodChainQueue[framework];
							var next = methodChainQueue.PendingNodesToRun.Dequeue();
							Log("[" + framework.UserAgentName + "] Executing: " + next.GetType().Name);

							tasks.Add(next.ExecuteAsync(framework));
						}

						await Task.WhenAll(tasks);
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

		public TMethodChainNode Enqueue<TMethodChainNode>(TMethodChainNode node) where TMethodChainNode : IBaseMethodChainNode
		{
			try
			{
				_semaphore.Wait();
				ThrowExceptionIfPresent();

				node.MethodChainContext = this;

				var isQueueEmpty = _nodeCount == 0;
				foreach (var framework in Frameworks)
				{
					var methodChainQueue = _userAgentMethodChainQueue[framework];
					var allNodes = methodChainQueue.AllNodes;

					var newNode = node.Clone();
					newNode.MethodChainContext = this;

					var linkedListNode = allNodes.AddLast(newNode);
					var parentNode = linkedListNode?.Previous?.Value;
					if (parentNode != null)
						newNode.SetParent(parentNode);

					methodChainQueue
						.PendingNodesToRun
						.Enqueue(newNode);
						
					Log("[" + framework.UserAgentName + "] Queued: " + newNode.GetType().Name);
				}

				_nodeCount++;

				if (isQueueEmpty)
					Task.Factory
						.StartNew(RunAllAsync);

				return node;
			}
			finally
			{
				_semaphore.Release(1);
			}
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

		public TaskAwaiter GetAwaiter()
		{
			ThrowExceptionIfPresent();
			return RunAllAsync().GetAwaiter();
		}
	}
}