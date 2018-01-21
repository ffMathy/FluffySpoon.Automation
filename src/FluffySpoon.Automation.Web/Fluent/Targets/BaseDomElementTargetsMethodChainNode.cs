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
	abstract class BaseDomElementTargetsMethodChainNode<TCurrentMethodChainNode, TNextMethodChainNode> :
		BaseDomElementTargetMethodChainNode<TCurrentMethodChainNode, TNextMethodChainNode>, 
		IBaseDomElementTargetsMethodChainNode<TCurrentMethodChainNode, TNextMethodChainNode> 
		where TNextMethodChainNode : IBaseMethodChainNode, new()
		where TCurrentMethodChainNode : IBaseMethodChainNode
	{
		public TNextMethodChainNode In(IReadOnlyList<IDomElement> elements) => Delegate(elements);
		public TNextMethodChainNode Of(IReadOnlyList<IDomElement> elements) => Delegate(elements);
		public TNextMethodChainNode From(IReadOnlyList<IDomElement> elements) => Delegate(elements);
		public TNextMethodChainNode On(IReadOnlyList<IDomElement> elements) => Delegate(elements);
		public TNextMethodChainNode At(IReadOnlyList<IDomElement> elements) => Delegate(elements);
	}
}
