using FluffySpoon.Automation.Web.Fluent.Root;
using System.Threading.Tasks;

namespace FluffySpoon.Automation.Web.Fluent.Open
{
	class OpenMethodChainNode: MethodChainRoot, IOpenMethodChainNode
	{
        private readonly string _uri;

        public OpenMethodChainNode(string uri)
        {
            _uri = uri;
        }

		protected override async Task OnExecuteAsync(IWebAutomationFrameworkInstance framework)
        {
            await framework.OpenAsync(_uri);
            await base.OnExecuteAsync(framework);
        }
    }
}