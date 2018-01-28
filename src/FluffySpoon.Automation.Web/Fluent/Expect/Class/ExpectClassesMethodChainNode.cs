using FluffySpoon.Automation.Web.Fluent.Targets;
using System.Threading.Tasks;

namespace FluffySpoon.Automation.Web.Fluent.Expect.Class
{
	class ExpectClassesMethodChainNode : BaseDomElementTargetsMethodChainNode<IBaseMethodChainNode, ExpectClassesMethodChainNode, ExpectClassesOfTargetsMethodChainNode>
	{
		internal string[] Classes { get; }

		public ExpectClassesMethodChainNode(params string[] classes)
		{
			Classes = classes;
		}

		protected override Task OnExecuteAsync(IWebAutomationFrameworkInstance framework)
		{
			return base.OnExecuteAsync(framework);
		}
	}
}
