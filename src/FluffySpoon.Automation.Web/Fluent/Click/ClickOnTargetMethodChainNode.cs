using FluffySpoon.Automation.Web.Fluent.Root;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace FluffySpoon.Automation.Web.Fluent.Click
{
    class ClickOnTargetMethodChainNode : MethodChainRoot<ClickMethodChainNode>, IClickOnTargetMethodChainNode
	{
		protected override async Task OnExecuteAsync(IWebAutomationFrameworkInstance framework)
		{
			await framework.ClickAsync(Parent.Elements, Parent.OffsetX, Parent.OffsetY);
			await base.OnExecuteAsync(framework);
		}
	}
}
