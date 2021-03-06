﻿using FluffySpoon.Automation.Web.Fluent.Targets;

namespace FluffySpoon.Automation.Web.Fluent.Expect.Class
{
	class ExpectClassesMethodChainNode : BaseDomElementTargetsMethodChainNode<IBaseMethodChainNode, ExpectClassesMethodChainNode, ExpectClassesOfTargetsMethodChainNode>
	{
		internal string[] Classes { get; }

		protected override bool MayCauseElementSideEffects => false;

		public ExpectClassesMethodChainNode(params string[] classes)
		{
			Classes = classes;
		}

		public override IBaseMethodChainNode Clone()
		{
			var clone = new ExpectClassesMethodChainNode(Classes);
			TransferDelegation(clone);

			return clone;
		}
	}
}
