﻿using System.Collections.Generic;
using FluffySpoon.Automation.Web.Fluent.Context;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Threading.Tasks;
using FluffySpoon.Automation.Web.Dom;

namespace FluffySpoon.Automation.Web.Fluent
{
	abstract class BaseMethodChainNode<TParentMethodChainNode> :
		IBaseMethodChainNode,
		IAwaitable<IReadOnlyList<IDomElement>>
		where TParentMethodChainNode : IBaseMethodChainNode
	{
		private readonly SemaphoreSlim _executeSemaphore;
		private IMethodChainContext _methodChainContext;

		public IMethodChainContext MethodChainContext { 
			protected get
			{
				return _methodChainContext;
			} 
			set
			{
				_methodChainContext = value;
				if(value != null)
					MethodChainOffset = value.NodeCount;
			}
		}
		public virtual IReadOnlyList<IDomElement> Elements { get; protected set; }

		public int MethodChainOffset { get; set; }

		protected internal TParentMethodChainNode Parent { get; private set; }

		public BaseMethodChainNode()
		{
			_executeSemaphore = new SemaphoreSlim(1);
		}

		public async Task ExecuteAsync(IWebAutomationFrameworkInstance framework)
		{
			await _executeSemaphore.WaitAsync();
			await OnExecuteAsync(framework);
			_executeSemaphore.Release(1);
		}

		protected virtual Task OnExecuteAsync(IWebAutomationFrameworkInstance framework)
		{
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
				.ContinueWith(t => Elements)
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