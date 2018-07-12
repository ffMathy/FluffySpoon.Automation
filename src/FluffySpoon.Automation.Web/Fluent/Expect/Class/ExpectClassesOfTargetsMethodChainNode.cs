using FluffySpoon.Automation.Web.Dom;
using FluffySpoon.Automation.Web.Exceptions;
using FluffySpoon.Automation.Web.Fluent.Expect.Root;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FluffySpoon.Automation.Web.Fluent.Expect.Class
{
	class ExpectClassesOfTargetsMethodChainNode : ExpectMethodChainRoot<ExpectClassesMethodChainNode>, IExpectClassesOfTargetsMethodChainNode
	{
		public override IReadOnlyList<IDomElement> Elements
		{
			get => Parent?.Elements;
		}

		protected override async Task OnExecuteAsync(IWebAutomationFrameworkInstance framework)
		{
			foreach(var element in Elements)
			{
				var classAttribute = element.Attributes["class"];
				var elementClasses = GetClassesFromAttributeValue(classAttribute);
				foreach(var classToFind in Parent.Classes) {
					if (!elementClasses.Contains(classToFind))
						throw ExpectationNotMetException.FromMethodChainNode(this, framework.UserAgentName, "The class \"" + classToFind + "\" was not found in the element.");
				}
			}

			await base.OnExecuteAsync(framework);
		}

		private static string[] GetClassesFromAttributeValue(string classAttribute)
		{
			return classAttribute.Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
		}

		public override IBaseMethodChainNode Clone()
		{
			return new ExpectClassesOfTargetsMethodChainNode();
		}
	}
}
