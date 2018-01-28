using FluffySpoon.Automation.Web.Fluent.Targets;
using System.Threading.Tasks;

namespace FluffySpoon.Automation.Web.Fluent.Expect.Text
{
	class ExpectTextMethodChainNode : BaseDomElementTargetsMethodChainNode<IBaseMethodChainNode, ExpectTextMethodChainNode, ExpectTextInTargetsMethodChainNode>
	{
		internal string Text { get; }

		public ExpectTextMethodChainNode(string text)
		{
			Text = text;
		}

		protected override Task OnExecuteAsync(IWebAutomationFrameworkInstance framework)
		{
			return base.OnExecuteAsync(framework);
		}
	}
}
