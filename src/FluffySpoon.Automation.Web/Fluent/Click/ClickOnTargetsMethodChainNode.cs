using FluffySpoon.Automation.Web.Dom;
using FluffySpoon.Automation.Web.Fluent.Root;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace FluffySpoon.Automation.Web.Fluent.Click
{
    class ClickOnTargetsMethodChainNode : MethodChainRoot<ClickMethodChainNode>, IClickOnTargetsMethodChainNode
	{
		public override IReadOnlyList<IDomElement> Elements { 
			get => Parent.Elements; 
		}

		protected override async Task OnExecuteAsync(IWebAutomationFrameworkInstance framework)
		{
			await framework.ClickAsync(Elements, Parent.OffsetX, Parent.OffsetY);
			await base.OnExecuteAsync(framework);
		}
	}
}
