using FluffySpoon.Automation.Web.Fluent.Targets.At;
using FluffySpoon.Automation.Web.Fluent.Targets.From;
using FluffySpoon.Automation.Web.Fluent.Targets.In;
using FluffySpoon.Automation.Web.Fluent.Targets.Of;
using FluffySpoon.Automation.Web.Fluent.Targets.On;

namespace FluffySpoon.Automation.Web.Fluent.Targets
{
	interface IBaseDomElementTargetsMethodChainNode<TCurrentMethodChainNode, TNextMethodChainNode> :
		IBaseDomElementTargetMethodChainNode<TCurrentMethodChainNode, TNextMethodChainNode>,
		IDomElementInTargetsMethodChainNode<TCurrentMethodChainNode, TNextMethodChainNode>, 
		IDomElementOfTargetsMethodChainNode<TCurrentMethodChainNode, TNextMethodChainNode>, 
		IDomElementFromTargetsMethodChainNode<TCurrentMethodChainNode, TNextMethodChainNode>, 
		IDomElementOnTargetsMethodChainNode<TCurrentMethodChainNode, TNextMethodChainNode>, 
		IDomElementAtTargetsMethodChainNode<TCurrentMethodChainNode, TNextMethodChainNode>
		where TCurrentMethodChainNode : IBaseMethodChainNode
		where TNextMethodChainNode : IBaseMethodChainNode, new()
	{
	}
}