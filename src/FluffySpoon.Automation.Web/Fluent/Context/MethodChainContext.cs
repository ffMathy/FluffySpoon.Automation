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

		private Task _cachedRunAllTask;

		private Exception _lastEncounteredException;

		private readonly SemaphoreSlim _semaphore;

		private readonly IDictionary<IWebAutomationFrameworkInstance, MethodChain> _userAgentMethodChainQueue;

		public IEnumerable<IWebAutomationFrameworkInstance> Frameworks { get; private set; }

		public int NodeCount { get; private set; }

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

		public Task RunAllAsync()
		{
			lock (this)
			{
				ThrowExceptionIfPresent();

				if (_cachedRunAllTask != null)
					return _cachedRunAllTask;

				_cachedRunAllTask = ExecuteRunAllAsync()
					.ContinueWith(t =>
					{
						_cachedRunAllTask = null;
						ThrowExceptionIfPresent();
					});
				return _cachedRunAllTask;
			}
		}

		private async Task ExecuteRunAllAsync()
		{
			while (NodeCount > 0)
				await RunNextAsync();
		}

		public async Task RunNextAsync()
		{
			try
			{
				await _semaphore.WaitAsync();

				try
				{
					if (NodeCount > 0)
					{
						NodeCount--;

						var tasks = new List<Task>();
						foreach (var framework in Frameworks)
						{
							var methodChainQueue = _userAgentMethodChainQueue[framework];
							var next = methodChainQueue.PendingNodesToRun.Dequeue();

							var nextToString = next.ToString();
							Log("[" + framework.UserAgentName + "] Executing: " + nextToString);

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

				var isQueueEmpty = NodeCount == 0;
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
						
					Log("[" + framework.UserAgentName + "] Queued: " + newNode);
				}

				NodeCount++;

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