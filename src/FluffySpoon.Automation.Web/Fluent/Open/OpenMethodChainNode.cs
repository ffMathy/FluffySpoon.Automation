using System.Threading.Tasks;

namespace FluffySpoon.Automation.Web.Fluent.Open
{
    class OpenMethodChainNode: DefaultMethodChainNode, IOpenMethodChainNode
	{
        private readonly string _url;

        public OpenMethodChainNode(
            IMethodChainContext methodChainContext,
            string url) : base(methodChainContext)
        {
            _url = url;
        }

        protected override async Task OnExecuteAsync(IWebAutomationFrameworkInstance framework)
        {
            await framework.OpenAsync(_url);
            await base.OnExecuteAsync(framework);
        }
    }
}