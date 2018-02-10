using FluffySpoon.Automation.Web.Dom;
using FluffySpoon.Automation.Web.Fluent.Targets.At;
using FluffySpoon.Automation.Web.Fluent.Targets.From;
using FluffySpoon.Automation.Web.Fluent.Targets.In;
using FluffySpoon.Automation.Web.Fluent.Targets.Of;
using FluffySpoon.Automation.Web.Fluent.Targets.On;
using FluffySpoon.Automation.Web.Fluent.Targets.To;
using System;
using System.Collections.Generic;

namespace FluffySpoon.Automation.Web.Fluent.Targets
{
	abstract class BaseMouseTargetMethodChainNode<TParentMethodChainNode, TCurrentMethodChainNode, TNextMethodChainNode> :
		BaseDomElementTargetMethodChainNode<TParentMethodChainNode, TCurrentMethodChainNode, TNextMethodChainNode>,
		IMouseInTargetMethodChainNode<TCurrentMethodChainNode, TNextMethodChainNode>,
		IMouseOfTargetMethodChainNode<TCurrentMethodChainNode, TNextMethodChainNode>,
		IMouseToTargetMethodChainNode<TCurrentMethodChainNode, TNextMethodChainNode>,
		IMouseFromTargetMethodChainNode<TCurrentMethodChainNode, TNextMethodChainNode>,
		IMouseOnTargetMethodChainNode<TCurrentMethodChainNode, TNextMethodChainNode>,
		IMouseAtTargetMethodChainNode<TCurrentMethodChainNode, TNextMethodChainNode>
		where TNextMethodChainNode : IBaseMethodChainNode, new()
		where TCurrentMethodChainNode : IBaseMethodChainNode
		where TParentMethodChainNode : IBaseMethodChainNode
	{
		public int OffsetX { get; private set; }
		public int OffsetY { get; private set; }

		protected TNextMethodChainNode Delegate(int x, int y)
		{
			throw new NotImplementedException();
		}

		protected TNextMethodChainNode Delegate(IDomElement element, int relativeX, int relativeY)
		{
			return Delegate(new[] { element }, relativeX, relativeY);
		}

		protected TNextMethodChainNode Delegate(IReadOnlyList<IDomElement> elements, int relativeX, int relativeY)
		{
			OffsetX = relativeX;
			OffsetY = relativeY;
			return Delegate(elements);
		}

		public TNextMethodChainNode In(int x, int y) => Delegate(x, y);
		public TNextMethodChainNode In(IDomElement element, int relativeX, int relativeY) => Delegate(element, relativeX, relativeY);

		public TNextMethodChainNode Of(int x, int y) => Delegate(x, y);
		public TNextMethodChainNode Of(IDomElement element, int relativeX, int relativeY) => Delegate(element, relativeX, relativeY);

		public TNextMethodChainNode From(int x, int y) => Delegate(x, y);
		public TNextMethodChainNode From(IDomElement element, int relativeX, int relativeY) => Delegate(element, relativeX, relativeY);

		public TNextMethodChainNode On(int x, int y) => Delegate(x, y);
		public TNextMethodChainNode On(IDomElement element, int relativeX, int relativeY) => Delegate(element, relativeX, relativeY);

		public TNextMethodChainNode At(int x, int y) => Delegate(x, y);
		public TNextMethodChainNode At(IDomElement element, int relativeX, int relativeY) => Delegate(element, relativeX, relativeY);

		public TNextMethodChainNode To(int x, int y) => Delegate(x, y);
		public TNextMethodChainNode To(IDomElement element, int relativeX, int relativeY) => Delegate(element, relativeX, relativeY);
	}
}
