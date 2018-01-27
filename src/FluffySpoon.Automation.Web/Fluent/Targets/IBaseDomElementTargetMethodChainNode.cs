using FluffySpoon.Automation.Web.Fluent.Targets.At;
using FluffySpoon.Automation.Web.Fluent.Targets.From;
using FluffySpoon.Automation.Web.Fluent.Targets.In;
using FluffySpoon.Automation.Web.Fluent.Targets.Of;
using FluffySpoon.Automation.Web.Fluent.Targets.On;

namespace FluffySpoon.Automation.Web.Fluent.Targets
{
	interface IBaseDomElementTargetMethodChainNode<TCurrentMethodChainNode, TNextMethodChainNode> :
		IDomElementInTargetMethodChainNode<TCurrentMethodChainNode, TNextMethodChainNode>, 
		IDomElementOfTargetMethodChainNode<TCurrentMethodChainNode, TNextMethodChainNode>, 
		IDomElementFromTargetMethodChainNode<TCurrentMethodChainNode, TNextMethodChainNode>, 
		IDomElementOnTargetMethodChainNode<TCurrentMethodChainNode, TNextMethodChainNode>, 
		IDomElementAtTargetMethodChainNode<TCurrentMethodChainNode, TNextMethodChainNode>
		where TCurrentMethodChainNode : IBaseMethodChainNode
		where TNextMethodChainNode : IBaseMethodChainNode, new()
	{
	}
}