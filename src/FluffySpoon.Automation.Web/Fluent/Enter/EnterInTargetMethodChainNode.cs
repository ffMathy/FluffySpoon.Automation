using System.Collections.Generic;
using FluffySpoon.Automation.Web.Fluent.Root;
using System.Threading.Tasks;
using FluffySpoon.Automation.Web.Dom;

namespace FluffySpoon.Automation.Web.Fluent.Enter
{
    class EnterInTargetMethodChainNode: MethodChainRoot<EnterMethodChainNode>, IEnterInTargetMethodChainNode
	{
		protected override bool MayCauseElementSideEffects => true;

		public override IReadOnlyList<IDomElement> Elements
		{
			get => Parent.Elements;
		}

        protected override async Task OnExecuteAsync(IWebAutomationFrameworkInstance framework)
        {
            await framework.EnterTextInAsync(Elements, Parent.TextToEnter);
            await base.OnExecuteAsync(framework);
        }

		public override IBaseMethodChainNode Clone()
		{
			return new EnterInTargetMethodChainNode();
		}
	}
}
