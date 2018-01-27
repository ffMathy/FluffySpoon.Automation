using FluffySpoon.Automation.Web.Fluent.Targets;
using System.Threading.Tasks;

namespace FluffySpoon.Automation.Web.Fluent.Click
{
	class ClickMethodChainNode : BaseMouseTargetsMethodChainNode<ClickMethodChainNode, ClickOnTargetsMethodChainNode>
	{
		public ClickMethodChainNode()
		{

		}

		protected override Task OnExecuteAsync(IWebAutomationFrameworkInstance framework)
		{
			return base.OnExecuteAsync(framework);
		}
	}
}
