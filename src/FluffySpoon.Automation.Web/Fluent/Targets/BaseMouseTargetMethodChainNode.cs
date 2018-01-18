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
	abstract class BaseMouseTargetMethodChainNode<TNextMethodChainNode> :
		BaseDomElementTargetMethodChainNode<TNextMethodChainNode>,
		IMouseInTargetMethodChainNode<TNextMethodChainNode>,
		IMouseOfTargetMethodChainNode<TNextMethodChainNode>,
		IMouseFromTargetMethodChainNode<TNextMethodChainNode>,
		IMouseOnTargetMethodChainNode<TNextMethodChainNode>,
		IMouseAtTargetMethodChainNode<TNextMethodChainNode>
		where TNextMethodChainNode : IBaseMethodChainNode
	{
		public TNextMethodChainNode Delegate(int x, int y)
		{
			throw new NotImplementedException();
		}

		public TNextMethodChainNode Delegate(IDomElement element, int relativeX, int relativeY)
		{
			throw new NotImplementedException();
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
	}
}
