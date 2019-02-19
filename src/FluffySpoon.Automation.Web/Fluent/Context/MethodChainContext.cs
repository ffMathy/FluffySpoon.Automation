using FluffySpoon.Automation.Web.Exceptions;
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

			foreach (var framework in Frameworks)
			{
				_userAgentMethodChainQueue.Add(framework, new MethodChain());
			}
		}

		public async Task RunAllAsync()
		{
			await _runAllSemaphore.WaitAsync();

			try
			{
				if (_cachedRunAllTask != null)
				{
					await _cachedRunAllTask;
					return;
				}

				_cachedRunAllTask = ExecuteRunAllAsync();
				await _cachedRunAllTask;

				_cachedRunAllTask = null;
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

							tasks.Add(next.ExecuteAsync(framework));
						}

						foreach (var task in tasks)
							await task;
					}

				}
				catch (AggregateException ex)
				{
					if (ex.InnerExceptions.Count == 1)
						throw GetExceptionToThrow(ex.InnerExceptions.Single());

					throw GetExceptionToThrow(ex);
				}
				catch (Exception ex)
				{
					throw GetExceptionToThrow(ex);
				}
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
				}

				NodeCount++;

				return node;
			}
			finally
			{
				_runNextSemaphore.Release(1);
			}
		}

		private Exception GetExceptionToThrow(Exception exception)
		{
			if (exception == null)
				return null;

			if (exception is ExpectationNotMetException ex)
				return ex;

			return new ApplicationException(
				"An error occured while performing one of the automation operations.",
				exception);
		}

		public TaskAwaiter GetAwaiter()
		{
			return RunAllAsync()
				.GetAwaiter();
		}
	}
}