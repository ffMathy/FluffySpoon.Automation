using FluffySpoon.Automation.Web.Fluent.Targets;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace FluffySpoon.Automation.Web.Fluent.Select
{
    class SelectByMethodChainNode: BaseDomElementTargetsMethodChainNode<IBaseMethodChainNode, SelectByMethodChainNode, SelectByFromTargetMethodChainNode>, 
		ISelectByMethodChainNode
    {
		internal string[] Values { get; private set; }
		internal int[] Indices { get; private set; }
		internal string[] Texts { get; private set; }

		private SelectByMethodChainNode() { }
		
		public static SelectByMethodChainNode ByValues(string[] values)
		{
			return new SelectByMethodChainNode()
			{
				Values = values
			};
		}

		public static SelectByMethodChainNode ByTexts(string[] texts)
		{
			return new SelectByMethodChainNode()
			{
				Texts = texts
			};
		}

		public static SelectByMethodChainNode ByIndices(int[] indices)
		{
			return new SelectByMethodChainNode()
			{
				Indices = indices
			};
		}
	}
}
