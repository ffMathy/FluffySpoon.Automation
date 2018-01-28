using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using FluffySpoon.Automation.Web.Dom;
using FluffySpoon.Automation.Web.Fluent.Root;

namespace FluffySpoon.Automation.Web.Fluent.Find
{
	class FindMethodChainNode : MethodChainRoot
    {
		private readonly string _selector;

		internal IReadOnlyList<IDomElement> Elements { get; private set; }

		public FindMethodChainNode(string selector)
		{
			_selector = selector;
		}

		protected override async Task OnExecuteAsync(IWebAutomationFrameworkInstance framework)
		{
			if (Parent is FindMethodChainNode)
				throw new InvalidOperationException("It is not possible to have two Find operations after each other.");

			Elements = await framework.FindDomElementsBySelectorAsync(_selector);
			await base.OnExecuteAsync(framework);
		}
	}
}
