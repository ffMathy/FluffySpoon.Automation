using FluffySpoon.Automation.Web.Dom;
using FluffySpoon.Automation.Web.Fluent.Root;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FluffySpoon.Automation.Web.Fluent.Focus
{
	class FocusOnTargetMethodChainNode : MethodChainRoot<FocusMethodChainNode>, IFocusOnTargetMethodChainNode
	{
		protected override bool MayCauseElementSideEffects => true;

		public override IReadOnlyList<IDomElement> Elements { 
			get => Parent.Elements; 
		}

		protected override async Task OnExecuteAsync(IWebAutomationFrameworkInstance framework)
		{
			await framework.FocusAsync(Elements.First());
			await base.OnExecuteAsync(framework);
		}

		public override IBaseMethodChainNode Clone()
		{
			return new FocusOnTargetMethodChainNode();
		}
	}
}
