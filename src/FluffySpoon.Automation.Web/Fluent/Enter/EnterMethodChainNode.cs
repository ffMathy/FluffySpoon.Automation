using System.Threading.Tasks;
using FluffySpoon.Automation.Web.Fluent.Targets;

namespace FluffySpoon.Automation.Web.Fluent.Enter
{
	class EnterMethodChainNode: BaseDomElementTargetsMethodChainNode<IBaseMethodChainNode, EnterMethodChainNode, EnterInTargetMethodChainNode>
    {
        internal string TextToEnter { get; }

        public EnterMethodChainNode(string text)
        {
            TextToEnter = text;
        }

		protected override Task OnExecuteAsync(IWebAutomationFrameworkInstance framework)
		{
			return base.OnExecuteAsync(framework);
		}
	}
}