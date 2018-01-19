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
	abstract class BaseMouseTargetsMethodChainNode<TCurrentMethodChainNode, TNextMethodChainNode> :
		BaseMouseTargetMethodChainNode<TCurrentMethodChainNode, TNextMethodChainNode>,
		IMouseInTargetsMethodChainNode<TCurrentMethodChainNode, TNextMethodChainNode>,
		IMouseOfTargetsMethodChainNode<TCurrentMethodChainNode, TNextMethodChainNode>,
		IMouseFromTargetsMethodChainNode<TCurrentMethodChainNode, TNextMethodChainNode>,
		IMouseOnTargetsMethodChainNode<TCurrentMethodChainNode, TNextMethodChainNode>,
		IMouseAtTargetsMethodChainNode<TCurrentMethodChainNode, TNextMethodChainNode>
		where TNextMethodChainNode : IBaseMethodChainNode<TCurrentMethodChainNode>, new()
		where TCurrentMethodChainNode : IBaseMethodChainNode
	{
		protected TNextMethodChainNode Delegate(IReadOnlyList<IDomElement> elements, int relativeX, int relativeY)
		{
			throw new NotImplementedException();
		}

		public TNextMethodChainNode In(IReadOnlyList<IDomElement> elements, int relativeX, int relativeY) => Delegate(elements, relativeX, relativeY);
		public TNextMethodChainNode In(IReadOnlyList<IDomElement> elements) => Delegate(elements);

		public TNextMethodChainNode Of(IReadOnlyList<IDomElement> elements, int relativeX, int relativeY) => Delegate(elements, relativeX, relativeY);
		public TNextMethodChainNode Of(IReadOnlyList<IDomElement> elements) => Delegate(elements);

		public TNextMethodChainNode From(IReadOnlyList<IDomElement> elements, int relativeX, int relativeY) => Delegate(elements, relativeX, relativeY);
		public TNextMethodChainNode From(IReadOnlyList<IDomElement> elements) => Delegate(elements);

		public TNextMethodChainNode On(IReadOnlyList<IDomElement> elements, int relativeX, int relativeY) => Delegate(elements, relativeX, relativeY);
		public TNextMethodChainNode On(IReadOnlyList<IDomElement> elements) => Delegate(elements);

		public TNextMethodChainNode At(IReadOnlyList<IDomElement> elements, int relativeX, int relativeY) => Delegate(elements, relativeX, relativeY);
		public TNextMethodChainNode At(IReadOnlyList<IDomElement> elements) => Delegate(elements);
	}
}
