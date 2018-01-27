using System.Collections.Generic;
using FluffySpoon.Automation.Web.Fluent.Context;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Threading.Tasks;
using FluffySpoon.Automation.Web.Dom;

namespace FluffySpoon.Automation.Web.Fluent
{
    abstract class BaseMethodChainNode<TParentMethodChainNode> : IBaseMethodChainNode
		where TParentMethodChainNode : IBaseMethodChainNode
    {
		private Task _currentExecutionTask;

		private readonly SemaphoreSlim _executeSemaphore;
		
        public IMethodChainContext MethodChainContext { protected get; set; }
		public virtual IReadOnlyList<IDomElement> Elements { get; protected set; }

		protected internal TParentMethodChainNode Parent { get; private set; }

        public BaseMethodChainNode()
        {
			_executeSemaphore = new SemaphoreSlim(1);
        }

        public TaskAwaiter GetAwaiter()
        {
            return MethodChainContext.GetAwaiter();
        }

        public async Task ExecuteAsync(IWebAutomationFrameworkInstance framework)
		{
			await _executeSemaphore.WaitAsync();
			await ExecuteCachedAsync(framework);
			_executeSemaphore.Release(1);
		}

		private Task ExecuteCachedAsync(IWebAutomationFrameworkInstance framework)
		{
			if (_currentExecutionTask != null)
				return _currentExecutionTask;

			return _currentExecutionTask = OnExecuteAsync(framework);
		}

		protected virtual Task OnExecuteAsync(IWebAutomationFrameworkInstance framework)
        {
            return Task.CompletedTask;
        }

		public void SetParent(IBaseMethodChainNode parent)
		{
			Parent = (TParentMethodChainNode)parent;
		}
	}
}