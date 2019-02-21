﻿using System.Collections.Generic;
using FluffySpoon.Automation.Web.Dom;
using FluffySpoon.Automation.Web.Fluent.Root;

namespace FluffySpoon.Automation.Web.Fluent.Select
{
	public interface ISelectByFromTargetMethodChainNode : 
		IBaseMethodChainNode, 
		IMethodChainRoot,
        IAwaitable<IReadOnlyList<IDomElement>>
	{
	}
}