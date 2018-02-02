using FluffySpoon.Automation.Web.Dom;
using System.Collections.Generic;

namespace FluffySpoon.Automation.Web.Fluent.Targets
{
	abstract class BaseDomElementTargetsMethodChainNode<TParentMethodChainNode, TCurrentMethodChainNode, TNextMethodChainNode> :
		BaseDomElementTargetMethodChainNode<TParentMethodChainNode, TCurrentMethodChainNode, TNextMethodChainNode>, 
		IBaseDomElementTargetsMethodChainNode<TCurrentMethodChainNode, TNextMethodChainNode> 
		where TNextMethodChainNode : IBaseMethodChainNode, new()
		where TCurrentMethodChainNode : IBaseMethodChainNode
		where TParentMethodChainNode : IBaseMethodChainNode
	{
		public TNextMethodChainNode In(IReadOnlyList<IDomElement> elements) => Delegate(elements);
		public TNextMethodChainNode Of(IReadOnlyList<IDomElement> elements) => Delegate(elements);
		public TNextMethodChainNode From(IReadOnlyList<IDomElement> elements) => Delegate(elements);
		public TNextMethodChainNode On(IReadOnlyList<IDomElement> elements) => Delegate(elements);
		public TNextMethodChainNode At(IReadOnlyList<IDomElement> elements) => Delegate(elements);
		public TNextMethodChainNode To(IReadOnlyList<IDomElement> elements) => Delegate(elements);
	}
}
