﻿using FluffySpoon.Automation.Web.Fluent.Targets.At;
using FluffySpoon.Automation.Web.Fluent.Targets.From;
using FluffySpoon.Automation.Web.Fluent.Targets.In;
using FluffySpoon.Automation.Web.Fluent.Targets.Of;
using FluffySpoon.Automation.Web.Fluent.Targets.On;
using FluffySpoon.Automation.Web.Fluent.Targets.To;

namespace FluffySpoon.Automation.Web.Fluent.Targets
{
	interface IBaseDomElementTargetMethodChainNode<out TCurrentMethodChainNode, out TNextMethodChainNode> :
		IDomElementInTargetMethodChainNode<TCurrentMethodChainNode, TNextMethodChainNode>, 
		IDomElementToTargetMethodChainNode<TCurrentMethodChainNode, TNextMethodChainNode>,
		IDomElementOfTargetMethodChainNode<TCurrentMethodChainNode, TNextMethodChainNode>, 
		IDomElementFromTargetMethodChainNode<TCurrentMethodChainNode, TNextMethodChainNode>, 
		IDomElementOnTargetMethodChainNode<TCurrentMethodChainNode, TNextMethodChainNode>, 
		IDomElementAtTargetMethodChainNode<TCurrentMethodChainNode, TNextMethodChainNode>
		where TCurrentMethodChainNode : IBaseMethodChainNode
		where TNextMethodChainNode : IBaseMethodChainNode, new()
	{
	}
}