using FluffySpoon.Automation.Web.Fluent.Root;
using System;
using System.Threading.Tasks;

namespace FluffySpoon.Automation.Web.Fluent.Wait
{
	class WaitMethodChainNode : MethodChainRoot
    {
		private readonly TimeSpan _timespan;

		public WaitMethodChainNode(TimeSpan timespan)
		{
			_timespan = timespan;
		}

		protected override async Task OnExecuteAsync(IWebAutomationFrameworkInstance framework)
		{
			await Task.Delay(_timespan);
			await base.OnExecuteAsync(framework);
		}
	}
}
