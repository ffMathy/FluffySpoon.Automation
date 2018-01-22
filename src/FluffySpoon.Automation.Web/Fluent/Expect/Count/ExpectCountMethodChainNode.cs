using FluffySpoon.Automation.Web.Fluent.Targets;
using System;
using System.Collections.Generic;
using System.Text;

namespace FluffySpoon.Automation.Web.Fluent.Expect.Count
{
    class ExpectCountMethodChainNode : BaseDomElementTargetsMethodChainNode<ExpectCountMethodChainNode, ExpectCountOfTargetsMethodChainNode>
	{
		internal int Count { get; }

		public ExpectCountMethodChainNode(int count)
		{
			Count = count;
		}
    }
}
