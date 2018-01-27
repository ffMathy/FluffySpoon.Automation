using FluffySpoon.Automation.Web.Dom;
using System.Collections.Generic;

namespace FluffySpoon.Automation.Web.Fluent.Targets
{
	abstract class BaseDomElementTargetsMethodChainNode<TCurrentMethodChainNode, TNextMethodChainNode> :
		BaseDomElementTargetMethodChainNode<TCurrentMethodChainNode, TNextMethodChainNode>, 
		IBaseDomElementTargetsMethodChainNode<TCurrentMethodChainNode, TNextMethodChainNode> 
		where TNextMethodChainNode : class, IBaseMethodChainNode, new()
		where TCurrentMethodChainNode : IBaseMethodChainNode
	{
		public TNextMethodChainNode In(IReadOnlyList<IDomElement> elements) => Delegate(elements);
		public TNextMethodChainNode Of(IReadOnlyList<IDomElement> elements) => Delegate(elements);
		public TNextMethodChainNode From(IReadOnlyList<IDomElement> elements) => Delegate(elements);
		public TNextMethodChainNode On(IReadOnlyList<IDomElement> elements) => Delegate(elements);
		public TNextMethodChainNode At(IReadOnlyList<IDomElement> elements) => Delegate(elements);
	}
}
