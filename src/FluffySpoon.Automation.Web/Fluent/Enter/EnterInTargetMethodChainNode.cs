using System.Collections.Generic;
using FluffySpoon.Automation.Web.Fluent.Root;
using System.Threading.Tasks;
using FluffySpoon.Automation.Web.Dom;

namespace FluffySpoon.Automation.Web.Fluent.Enter
{
    class EnterInTargetMethodChainNode: MethodChainRoot, IEnterInTargetMethodChainNode
	{
        private readonly EnterMethodChainNode _parentNode;
        private readonly IReadOnlyList<IDomElement> _elements;

		public EnterInTargetMethodChainNode()
		{
			
		}

        protected override async Task OnExecuteAsync(IWebAutomationFrameworkInstance framework)
        {
            await framework.EnterTextInAsync(_elements, _parentNode.TextToEnter);
            await base.OnExecuteAsync(framework);
        }
    }
}
