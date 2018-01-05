using System.Threading.Tasks;

namespace FluffySpoon.Automation.Web.Fluent
{
    class OpenMethodChainNode: DefaultMethodChainNode, IOpenMethodChainNode
    {
        private readonly string _url;

        public OpenMethodChainNode(
            IMethodChainQueue methodChainQueue,
            string url) : base(methodChainQueue)
        {
            _url = url;
        }

        protected override async Task OnExecuteAsync(IWebAutomationTechnology technology)
        {
            await technology.OpenAsync(_url);
            await base.OnExecuteAsync(technology);
        }
    }
}