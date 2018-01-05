using System.Threading.Tasks;

namespace FluffySpoon.Automation.Web.Fluent
{
    class EnterInMethodChainNode: DefaultMethodChainNode
    {
        private readonly EnterMethodChainNode _parentNode;

        private readonly string _selector;

        public EnterInMethodChainNode(
            EnterMethodChainNode parentNode,
            IMethodChainContext methodChainContext, 
            string selector) : base(methodChainContext)
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
