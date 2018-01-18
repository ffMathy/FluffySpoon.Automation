using FluffySpoon.Automation.Web.Fluent.Root;
using System.Threading.Tasks;

namespace FluffySpoon.Automation.Web.Fluent.Enter
{
    class EnterInMethodChainNode: MethodChainRoot, IEnterInTargetMethodChainNode
	{
        private readonly EnterMethodChainNode _parentNode;

        private readonly string _selector;

        public EnterInMethodChainNode(
            EnterMethodChainNode parentNode,
            string selector)
        {
            _parentNode = parentNode;
            _selector = selector;
        }

        protected override async Task OnExecuteAsync(IWebAutomationFrameworkInstance framework)
        {
            await framework.EnterTextInAsync(_parentNode.TextToEnter, _selector);
            await base.OnExecuteAsync(framework);
        }
    }
}
