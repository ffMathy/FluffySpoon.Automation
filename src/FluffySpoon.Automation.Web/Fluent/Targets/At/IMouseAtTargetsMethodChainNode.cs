﻿using FluffySpoon.Automation.Web.Dom;
using System.Collections.Generic;

namespace FluffySpoon.Automation.Web.Fluent.Targets.At
{
	public interface IMouseAtTargetsMethodChainNode<TNextMethodChainNode> : 
		IMouseAtTargetMethodChainNode<TNextMethodChainNode>, 
		IDomElementAtTargetsMethodChainNode<TNextMethodChainNode> 
		where TNextMethodChainNode : IBaseMethodChainNode
	{
		TNextMethodChainNode At(IReadOnlyCollection<IDomElement> elements, int relativeX, int relativeY);
	}
}