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
	abstract class BaseDomElementTargetMethodChainNode<TNextMethodChainNode> :
		BaseMethodChainNode,
		IDomElementInTargetMethodChainNode<TNextMethodChainNode>,
		IDomElementOfTargetMethodChainNode<TNextMethodChainNode>,
		IDomElementFromTargetMethodChainNode<TNextMethodChainNode>,
		IDomElementOnTargetMethodChainNode<TNextMethodChainNode>,
		IDomElementAtTargetMethodChainNode<TNextMethodChainNode>
		where TNextMethodChainNode : IBaseMethodChainNode
	{
		protected TNextMethodChainNode Delegate(string selector)
		{
			return MethodChainContext.Enqueue(new TNextMethodChainNode(
				this,
				selector));
		}

		protected TNextMethodChainNode Delegate(IDomElement element)
		{
			return In(new[] { element });
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
