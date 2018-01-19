using System;
using System.Collections.Generic;
using System.Text;

namespace FluffySpoon.Automation.Web.Fluent.Find
{
    class FindMethodToTargetMethodChainNode
    {
		private readonly FindMethodChainNode _parent;

		public FindMethodToTargetMethodChainNode(FindMethodChainNode parent)
		{
			_parent = parent;
		}
    }
}
