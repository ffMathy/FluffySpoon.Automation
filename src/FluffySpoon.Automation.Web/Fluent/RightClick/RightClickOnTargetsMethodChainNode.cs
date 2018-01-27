using FluffySpoon.Automation.Web.Dom;
using FluffySpoon.Automation.Web.Fluent.Root;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace FluffySpoon.Automation.Web.Fluent.RightClick
{
    class RightClickOnTargetsMethodChainNode : MethodChainRoot<RightClickMethodChainNode>, IRightClickOnTargetsMethodChainNode
	{
		public override IReadOnlyList<IDomElement> Elements
		{
			get => Parent.Elements;
		}

		protected override async Task OnExecuteAsync(IWebAutomationFrameworkInstance framework)
		{
			await framework.RightClickAsync(Elements, Parent.OffsetX, Parent.OffsetY);
			await base.OnExecuteAsync(framework);
		}
	}
}
