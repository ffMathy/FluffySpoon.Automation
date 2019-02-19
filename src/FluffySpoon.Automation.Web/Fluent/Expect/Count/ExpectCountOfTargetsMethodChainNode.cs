using FluffySpoon.Automation.Web.Dom;
using FluffySpoon.Automation.Web.Exceptions;
using FluffySpoon.Automation.Web.Fluent.Expect.Root;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FluffySpoon.Automation.Web.Fluent.Expect.Count
{
	class ExpectCountOfTargetsMethodChainNode : ExpectMethodChainRoot<ExpectCountMethodChainNode>, IExpectCountOfTargetsMethodChainNode
	{
		public override IReadOnlyList<IDomElement> Elements
		{
			get => Parent?.Elements;
		}

		public ExpectCountOfTargetsMethodChainNode()
		{

		}

		protected override async Task OnExecuteAsync(IWebAutomationFrameworkInstance framework)
		{
			Console.WriteLine("BeforeExecute");

			if (Elements?.Count != Parent.Count)
				throw ExpectationNotMetException.FromMethodChainNode(this, framework.UserAgentName, "Expected " + Parent.Count + " elements but found " + (Parent.Elements?.Count ?? 0) + ".");

			await base.OnExecuteAsync(framework);

			Console.WriteLine("AfterExecute");
		}

		public override IBaseMethodChainNode Clone()
		{
			return new ExpectCountOfTargetsMethodChainNode();
		}
	}
}
