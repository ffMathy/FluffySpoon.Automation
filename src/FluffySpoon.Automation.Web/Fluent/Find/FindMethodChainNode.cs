using System.Threading.Tasks;

namespace FluffySpoon.Automation.Web.Fluent.Find
{
	class FindMethodChainNode :
		BaseMethodChainNode<IBaseMethodChainNode>, IFindMethodChainNode
	{
		private readonly string _selector;

		private FindMethodChainNode DelegatedFrom { get; set; }

		public FindMethodChainNode(string selector)
		{
			_selector = selector;
		}

		protected override async Task OnExecuteAsync(IWebAutomationFrameworkInstance framework)
		{
			Elements = await framework.FindDomElementsBySelectorAsync(
				MethodChainOffset,
				_selector);

			if (DelegatedFrom != null)
				DelegatedFrom.Elements = Elements;
		}

		public override IBaseMethodChainNode Clone()
		{
			return new FindMethodChainNode(_selector) {
				DelegatedFrom = this
			};
		}
	}
}
