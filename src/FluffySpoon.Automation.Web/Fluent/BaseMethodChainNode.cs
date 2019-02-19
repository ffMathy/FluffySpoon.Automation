using System.Collections.Generic;
using FluffySpoon.Automation.Web.Fluent.Context;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Threading.Tasks;
using FluffySpoon.Automation.Web.Dom;
using System;
using System.Linq;

namespace FluffySpoon.Automation.Web.Fluent
{
	abstract class BaseMethodChainNode<TParentMethodChainNode> :
		IBaseMethodChainNode,
		IAwaitable<IReadOnlyList<IDomElement>>
		where TParentMethodChainNode : IBaseMethodChainNode
	{
		private readonly SemaphoreSlim _executeSemaphore;

		private IMethodChainContext _methodChainContext;
		private IReadOnlyList<IDomElement> _elements;

		public IMethodChainContext MethodChainContext
		{
			protected get
			{
				return _methodChainContext;
			}
			set
			{
				_methodChainContext = value;
				if (value != null)
					MethodChainOffset = value.NodeCount;
			}
		}

		public virtual IReadOnlyList<IDomElement> Elements
		{
			get => _elements;
			protected set => _elements = value;
		}

		public int MethodChainOffset { get; set; }

		protected internal TParentMethodChainNode Parent
		{
			get;
			private set;
		}

		public BaseMethodChainNode()
		{
			_executeSemaphore = new SemaphoreSlim(1);
		}

		public async Task ExecuteAsync(IWebAutomationFrameworkInstance framework)
		{
			await _executeSemaphore.WaitAsync();
			try
			{
				await OnExecuteAsync(framework);
			}
			finally
			{
				_executeSemaphore.Release(1);
			}
		}

		protected virtual Task OnExecuteAsync(IWebAutomationFrameworkInstance framework)
		{
			Console.WriteLine("OnExecute");
			return Task.CompletedTask;
		}

		public void SetParent(IBaseMethodChainNode parent)
		{
			Parent = (TParentMethodChainNode)parent;
		}

		public abstract IBaseMethodChainNode Clone();

		public TaskAwaiter<IReadOnlyList<IDomElement>> GetAwaiter()
		{
			return MethodChainContext
				.RunAllAsync()
				.ContinueWith(
					t =>
					{
						if (t.Exception != null)
						{
							throw t.Exception.InnerExceptions.Count == 1 ?
								t.Exception.InnerExceptions.Single() :
								t.Exception;
						}

						return Elements;
					},
					TaskUtilities.ContinuationOptions)
				.GetAwaiter();
		}

		public override string ToString()
		{
			var result = string.Empty;
			if (Parent != null)
				result += Parent.ToString() + " -> ";

			result += GetType().Name.Replace("MethodChainNode", "");
			return result;
		}
	}
}