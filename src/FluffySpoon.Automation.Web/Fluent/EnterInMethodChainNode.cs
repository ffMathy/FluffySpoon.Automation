using System.Threading.Tasks;

namespace FluffySpoon.Automation.Web.Fluent
{
    class EnterInMethodChainNode: DefaultMethodChainNode
    {
        private readonly IEnterMethodChainNode _parentNode;

        private readonly string _selector;

        public EnterInMethodChainNode(
            IEnterMethodChainNode parentNode,
            IMethodChainQueue methodChainQueue, 
            string selector) : base(methodChainQueue)
        {
            _parentNode = parentNode;
            _selector = selector;
        }

        protected override async Task OnExecuteAsync(IWebAutomationTechnology technology)
        {
            await technology.EnterTextIn(_parentNode.TextToEnter, _selector);
            await base.OnExecuteAsync(technology);
        }
    }
}
