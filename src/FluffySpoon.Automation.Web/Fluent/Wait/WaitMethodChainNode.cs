using FluffySpoon.Automation.Web.Fluent.Root;
using System;
using System.Threading.Tasks;

namespace FluffySpoon.Automation.Web.Fluent.Wait
{
	class WaitMethodChainNode : MethodChainRoot<IBaseMethodChainNode>, IWaitMethodChainNode
	{
		private readonly Func<Task<bool>> _predicate;

		public WaitMethodChainNode(Func<Task<bool>> predicate)
		{
			_predicate = predicate;
		}

		protected override async Task OnExecuteAsync(IWebAutomationFrameworkInstance framework)
		{
			while (!await _predicate())
				await Task.Delay(100);

			await base.OnExecuteAsync(framework);
		}

		public override IBaseMethodChainNode Clone()
		{
			return new WaitMethodChainNode(_predicate);
		}
	}
}
