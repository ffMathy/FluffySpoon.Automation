using FluffySpoon.Automation.Web.Fluent.Targets;
using System.Threading.Tasks;

namespace FluffySpoon.Automation.Web.Fluent.Expect.Value
{
	class ExpectValueMethodChainNode : BaseDomElementTargetsMethodChainNode<IBaseMethodChainNode, ExpectValueMethodChainNode, ExpectValueInTargetsMethodChainNode>
	{
		internal string Value { get; }

		public ExpectValueMethodChainNode(string text)
		{
			Value = text;
		}

		protected override Task OnExecuteAsync(IWebAutomationFrameworkInstance framework)
		{
			return base.OnExecuteAsync(framework);
		}
	}
}
