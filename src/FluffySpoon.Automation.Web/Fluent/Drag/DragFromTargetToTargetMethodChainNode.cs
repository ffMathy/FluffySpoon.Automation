using FluffySpoon.Automation.Web.Dom;
using FluffySpoon.Automation.Web.Fluent.Root;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FluffySpoon.Automation.Web.Fluent.Drag
{
	internal class DragFromTargetToTargetMethodChainNode : MethodChainRoot<DragFromTargetMethodChainNode>, IDragFromTargetToTargetMethodChainNode
	{
		protected override async Task OnExecuteAsync(IWebAutomationFrameworkInstance framework)
		{
			await framework.DragDropAsync(Parent.Elements.Single(), Parent.Elements.Single());
			await base.OnExecuteAsync(framework);
		}
	}
}