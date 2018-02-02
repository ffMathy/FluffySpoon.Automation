﻿using FluffySpoon.Automation.Web.Fluent.Targets;

namespace FluffySpoon.Automation.Web.Fluent.Select
{
    class SelectByMethodChainNode: BaseDomElementTargetsMethodChainNode<IBaseMethodChainNode, SelectByMethodChainNode, SelectByFromTargetMethodChainNode>
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

		public override IBaseMethodChainNode Clone()
		{
			return new SelectByMethodChainNode() {
				Indices = Indices,
				Texts = Texts,
				Values = Values
			};
		}
	}
}
