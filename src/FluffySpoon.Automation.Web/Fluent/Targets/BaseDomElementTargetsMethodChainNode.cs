using FluffySpoon.Automation.Web.Dom;
using FluffySpoon.Automation.Web.Fluent.Targets.At;
using FluffySpoon.Automation.Web.Fluent.Targets.From;
using FluffySpoon.Automation.Web.Fluent.Targets.In;
using FluffySpoon.Automation.Web.Fluent.Targets.Of;
using FluffySpoon.Automation.Web.Fluent.Targets.On;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FluffySpoon.Automation.Web.Fluent.Targets
{
	abstract class BaseDomElementTargetsMethodChainNode<TNextMethodChainNode> :
		BaseDomElementTargetMethodChainNode<TNextMethodChainNode>,
		IDomElementInTargetsMethodChainNode<TNextMethodChainNode>,
		IDomElementOfTargetsMethodChainNode<TNextMethodChainNode>,
		IDomElementFromTargetsMethodChainNode<TNextMethodChainNode>,
		IDomElementOnTargetsMethodChainNode<TNextMethodChainNode>,
		IDomElementAtTargetsMethodChainNode<TNextMethodChainNode>
		where TNextMethodChainNode : IBaseMethodChainNode
	{
		protected TNextMethodChainNode Delegate(IReadOnlyCollection<IDomElement> elements)
		{
			return Delegate(elements
				.Select(x => x.CssSelector)
				.Aggregate((a, b) => a + ", " + b));
		}

		public TNextMethodChainNode In(IReadOnlyCollection<IDomElement> elements) => Delegate(elements);
		public TNextMethodChainNode Of(IReadOnlyCollection<IDomElement> elements) => Delegate(elements);
		public TNextMethodChainNode From(IReadOnlyCollection<IDomElement> elements) => Delegate(elements);
		public TNextMethodChainNode On(IReadOnlyCollection<IDomElement> elements) => Delegate(elements);
		public TNextMethodChainNode At(IReadOnlyCollection<IDomElement> elements) => Delegate(elements);
	}
}
