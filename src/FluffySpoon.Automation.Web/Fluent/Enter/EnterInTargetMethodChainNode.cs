using System.Collections.Generic;
using FluffySpoon.Automation.Web.Fluent.Root;
using System.Threading.Tasks;
using FluffySpoon.Automation.Web.Dom;

namespace FluffySpoon.Automation.Web.Fluent.Enter
{
    class EnterInTargetMethodChainNode: MethodChainRoot<EnterMethodChainNode>, IEnterInTargetMethodChainNode
	{
		public EnterInTargetMethodChainNode()
		{
			
		}

        protected override async Task OnExecuteAsync(IWebAutomationFrameworkInstance framework)
        {
            await framework.EnterTextInAsync(Parent.Elements, Parent.TextToEnter);
            await base.OnExecuteAsync(framework);
        }
	}
}
