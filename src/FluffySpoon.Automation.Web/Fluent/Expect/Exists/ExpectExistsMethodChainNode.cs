using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace FluffySpoon.Automation.Web.Fluent.Expect.Exists
{
	class ExpectExistsMethodChainNode : BaseMethodChainNode, IExpectExistsMethodChainNode
	{
		private readonly string _selector;

		public ExpectExistsMethodChainNode(string selector) {
			_selector = selector;
		}

		public async Task ExecuteAsync(IWebAutomationFrameworkInstance framework)
		{
			var elements = await framework.FindDomElementsAsync(_selector); 
		}

		public TaskAwaiter GetAwaiter()
		{
			throw new NotImplementedException();
		}
	}
}
