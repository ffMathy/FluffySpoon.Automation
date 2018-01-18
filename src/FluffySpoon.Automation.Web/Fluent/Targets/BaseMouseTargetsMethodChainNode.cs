using FluffySpoon.Automation.Web.Dom;
using FluffySpoon.Automation.Web.Fluent.Targets.At;
using FluffySpoon.Automation.Web.Fluent.Targets.From;
using FluffySpoon.Automation.Web.Fluent.Targets.In;
using FluffySpoon.Automation.Web.Fluent.Targets.Of;
using FluffySpoon.Automation.Web.Fluent.Targets.On;
using System;
using System.Collections.Generic;
using System.Text;

namespace FluffySpoon.Automation.Web.Fluent.Targets
{
	abstract class BaseMouseTargetsMethodChainNode<TNextMethodChainNode> :
		BaseMouseTargetMethodChainNode<TNextMethodChainNode>,
		IMouseInTargetsMethodChainNode<TNextMethodChainNode>,
		IMouseOfTargetsMethodChainNode<TNextMethodChainNode>,
		IMouseFromTargetsMethodChainNode<TNextMethodChainNode>,
		IMouseOnTargetsMethodChainNode<TNextMethodChainNode>,
		IMouseAtTargetsMethodChainNode<TNextMethodChainNode>
		where TNextMethodChainNode : IBaseMethodChainNode
	{
		public TNextMethodChainNode Delegate(IReadOnlyCollection<IDomElement> elements, int relativeX, int relativeY)
		{
			throw new NotImplementedException();
		}

		public TNextMethodChainNode Delegate(IReadOnlyCollection<IDomElement> elements)
		{
			throw new NotImplementedException();
		}

		public TNextMethodChainNode In(IReadOnlyCollection<IDomElement> elements, int relativeX, int relativeY) => Delegate(elements, relativeX, relativeY);
		public TNextMethodChainNode In(IReadOnlyCollection<IDomElement> elements) => Delegate(elements);

		public TNextMethodChainNode Of(IReadOnlyCollection<IDomElement> elements, int relativeX, int relativeY) => Delegate(elements, relativeX, relativeY);
		public TNextMethodChainNode Of(IReadOnlyCollection<IDomElement> elements) => Delegate(elements);

		public TNextMethodChainNode From(IReadOnlyCollection<IDomElement> elements, int relativeX, int relativeY) => Delegate(elements, relativeX, relativeY);
		public TNextMethodChainNode From(IReadOnlyCollection<IDomElement> elements) => Delegate(elements);

		public TNextMethodChainNode On(IReadOnlyCollection<IDomElement> elements, int relativeX, int relativeY) => Delegate(elements, relativeX, relativeY);
		public TNextMethodChainNode On(IReadOnlyCollection<IDomElement> elements) => Delegate(elements);

		public TNextMethodChainNode At(IReadOnlyCollection<IDomElement> elements, int relativeX, int relativeY) => Delegate(elements, relativeX, relativeY);
		public TNextMethodChainNode At(IReadOnlyCollection<IDomElement> elements) => Delegate(elements);
	}
}
