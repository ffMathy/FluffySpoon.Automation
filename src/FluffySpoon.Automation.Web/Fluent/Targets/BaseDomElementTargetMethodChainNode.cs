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
using FluffySpoon.Automation.Web.Fluent.Find;

namespace FluffySpoon.Automation.Web.Fluent.Targets
{
	abstract class BaseDomElementTargetMethodChainNode<TCurrentMethodChainNode, TNextMethodChainNode> :
		BaseMethodChainNode,
		IDomElementInTargetMethodChainNode<TCurrentMethodChainNode, TNextMethodChainNode>,
		IDomElementOfTargetMethodChainNode<TCurrentMethodChainNode, TNextMethodChainNode>,
		IDomElementFromTargetMethodChainNode<TCurrentMethodChainNode, TNextMethodChainNode>,
		IDomElementOnTargetMethodChainNode<TCurrentMethodChainNode, TNextMethodChainNode>,
		IDomElementAtTargetMethodChainNode<TCurrentMethodChainNode, TNextMethodChainNode>
		where TNextMethodChainNode : IBaseMethodChainNode<TCurrentMethodChainNode>, new()
		where TCurrentMethodChainNode : IBaseMethodChainNode
	{
		protected IReadOnlyList<IDomElement> Elements { get; private set; }

		protected TNextMethodChainNode Delegate(string selector)
		{
			MethodChainContext.Enqueue(new FindMethodChainNode(selector));
			return MethodChainContext.Enqueue(new TNextMethodChainNode());
		}

		protected TNextMethodChainNode Delegate(IDomElement element)
		{
			return Delegate(new[] { element });
		}

		protected TNextMethodChainNode Delegate(IReadOnlyList<IDomElement> elements)
		{
			Elements = elements;
			MethodChainContext.Enqueue(this);
			return MethodChainContext.Enqueue(new TNextMethodChainNode());
		}

		public TNextMethodChainNode In(string selector) => Delegate(selector);
		public TNextMethodChainNode In(IDomElement element) => Delegate(element);

		public TNextMethodChainNode Of(string selector) => Delegate(selector);
		public TNextMethodChainNode Of(IDomElement element) => Delegate(element);

		public TNextMethodChainNode From(string selector) => Delegate(selector);
		public TNextMethodChainNode From(IDomElement element) => Delegate(element);

		public TNextMethodChainNode On(string selector) => Delegate(selector);
		public TNextMethodChainNode On(IDomElement element) => Delegate(element);

		public TNextMethodChainNode At(string selector) => Delegate(selector);
		public TNextMethodChainNode At(IDomElement element) => Delegate(element);
	}
}
