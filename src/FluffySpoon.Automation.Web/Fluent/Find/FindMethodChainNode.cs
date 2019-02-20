using FluffySpoon.Automation.Web.Fluent.Targets;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace FluffySpoon.Automation.Web.Fluent.Find
{
	class FindMethodChainNode :
		BaseMethodChainNode<IBaseMethodChainNode>, IFindMethodChainNode
	{
		private readonly string selector;

		protected override bool MayCauseElementSideEffects => false;

		private FindMethodChainNode DelegatedFrom { get; set; }

		public FindMethodChainNode(string selector)
		{
			this.selector = selector;
		}

		protected override async Task OnExecuteAsync(IWebAutomationFrameworkInstance framework)
		{
			Elements = await framework.FindDomElementsBySelectorAsync(
				MethodChainOffset,
				selector);

			if (DelegatedFrom != null)
				DelegatedFrom.Elements = Elements;
		}

		public override IBaseMethodChainNode Clone()
		{
			return new FindMethodChainNode(selector) {
				DelegatedFrom = this
			};
		}
	}
}
