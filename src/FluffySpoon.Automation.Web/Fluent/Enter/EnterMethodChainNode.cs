using System.Collections.Generic;
using System.Linq;
using FluffySpoon.Automation.Web.Dom;
using FluffySpoon.Automation.Web.Fluent.Root;
using FluffySpoon.Automation.Web.Fluent.Targets.In;

namespace FluffySpoon.Automation.Web.Fluent.Enter
{
    class EnterMethodChainNode: BaseMethodChainNode, IEnterMethodChainNode
    {
        internal string TextToEnter { get; }

        public EnterMethodChainNode(string text)
        {
            TextToEnter = text;
        }

        public IEnterInTargetMethodChainNode In(string selector)
        {
            return MethodChainContext.Enqueue(new EnterInMethodChainNode(
                this,
                selector));
        }

        public IEnterInTargetMethodChainNode In(IDomElement element)
        {
            return In(new[] { element });
        }

		public IEnterInTargetMethodChainNode In(IReadOnlyCollection<IDomElement> elements)
		{
			return In(elements
				.Select(x => x.CssSelector)
				.Aggregate((a, b) => a + ", " + b));
		}
	}
}