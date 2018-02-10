using FluffySpoon.Automation.Web.Dom;
using FluffySpoon.Automation.Web.Fluent.Root;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FluffySpoon.Automation.Web.Fluent.Hover
{
	class HoverOnTargetMethodChainNode : MethodChainRoot<HoverMethodChainNode>, IHoverOnTargetMethodChainNode
	{
		public override IReadOnlyList<IDomElement> Elements { 
			get => Parent.Elements; 
		}

		protected override async Task OnExecuteAsync(IWebAutomationFrameworkInstance framework)
		{
			await framework.HoverAsync(Elements.First(), Parent.OffsetX, Parent.OffsetY);
			await base.OnExecuteAsync(framework);
		}

		public override IBaseMethodChainNode Clone()
		{
			return new HoverOnTargetMethodChainNode();
		}
	}
}
