using FluffySpoon.Automation.Web.Fluent.Root;
using System.Threading.Tasks;

namespace FluffySpoon.Automation.Web.Fluent.Open
{
	class OpenMethodChainNode: MethodChainRoot<IBaseMethodChainNode>, IOpenMethodChainNode
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

		public override IBaseMethodChainNode Clone()
		{
			return new OpenMethodChainNode(_uri);
		}
	}
}