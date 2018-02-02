using System;
using System.Collections.Generic;
using System.Text;

namespace FluffySpoon.Automation.Web.Fluent.Root
{
    class MethodChainEntryPoint: MethodChainRoot<IBaseMethodChainNode>
    {
		public override IBaseMethodChainNode Clone()
		{
			return this;
		}
	}
}
