using FluffySpoon.Automation.Web.Exceptions;
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

		private readonly SemaphoreSlim _runNextSemaphore;
		private readonly SemaphoreSlim _runAllSemaphore;

		private readonly IDictionary<IWebAutomationFrameworkInstance, MethodChain> _userAgentMethodChainQueue;

		public IWebAutomationEngine AutomationEngine { get; private set; }

		public IEnumerable<IWebAutomationFrameworkInstance> Frameworks
		{
			get;
			private set;
		}

		public int NodeCount { get; private set; }

		public MethodChainContext(
			IEnumerable<IWebAutomationFrameworkInstance> frameworks,
			IWebAutomationEngine automationEngine)
		{
			_userAgentMethodChainQueue = new Dictionary<IWebAutomationFrameworkInstance, MethodChain>();

			_runNextSemaphore = new SemaphoreSlim(1);
			_runAllSemaphore = new SemaphoreSlim(1);

			Frameworks = frameworks;
			AutomationEngine = automationEngine;

			_methodChainOffset = Interlocked.Increment(ref _globalMethodChainOffset);

			foreach (var framework in Frameworks)
			{
				_userAgentMethodChainQueue.Add(framework, new MethodChain());
			}
		}

		public async Task RunAllAsync()
		{
			await _runAllSemaphore.WaitAsync();

			ThrowExceptionIfPresent();

			try
			{
				if (_cachedRunAllTask != null) { 
					await _cachedRunAllTask;
					return;
				}

				_cachedRunAllTask = ExecuteRunAllAsync();
				await _cachedRunAllTask;

				_cachedRunAllTask = null;
				ThrowExceptionIfPresent();
			}
			finally
			{
				_runAllSemaphore.Release(1);
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
				await _runNextSemaphore.WaitAsync();

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
							var nextNext = methodChainQueue.PendingNodesToRun.Count > 0 ?
								methodChainQueue.PendingNodesToRun.Peek() :
								null;

							Log("[" + framework.UserAgentName + "] Executing: " + next);

							tasks.Add(next.ExecuteAsync(framework));

							Log("[" + framework.UserAgentName + "] Executed: " + next);

							if (nextNext != null)
								Log("[" + framework.UserAgentName + "] Next is: " + nextNext);
						}

						await Task.WhenAll(tasks);
					}

				}
				catch (AggregateException ex)
				{
					AutomationEngine.LastEncounteredException =
					   ex.InnerExceptions.Count == 1 ? ex.InnerExceptions.Single() : ex;
				}
				catch (Exception ex)
				{
					AutomationEngine.LastEncounteredException = ex;
				}

				ThrowExceptionIfPresent();
			}
			finally
			{
				_runNextSemaphore.Release(1);
			}
		}

		public TMethodChainNode Enqueue<TMethodChainNode>(TMethodChainNode node) where TMethodChainNode : IBaseMethodChainNode
		{
			try
			{
				_runNextSemaphore.Wait();

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
					{
						newNode.SetParent(parentNode);
						node.SetParent(parentNode);
					}

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
				_runNextSemaphore.Release(1);
			}
		}

		private void ThrowExceptionIfPresent()
		{
			var exception = GetExceptionToThrow();
			if (exception == null)
				return;

			throw exception;
		}

		private Exception GetExceptionToThrow()
		{
			if (AutomationEngine.LastEncounteredException == null)
				return null;

			if (AutomationEngine.LastEncounteredException is ExpectationNotMetException ex)
				return ex;

			return new ApplicationException(
				"An error occured while performing one of the automation operations.",
				AutomationEngine.LastEncounteredException);
		}

		private void Log(string message)
		{
			Console.WriteLine("[#" + _methodChainOffset + "] " + message);
		}

		public TaskAwaiter GetAwaiter()
		{
			return RunAllAsync().GetAwaiter();
		}
	}
}