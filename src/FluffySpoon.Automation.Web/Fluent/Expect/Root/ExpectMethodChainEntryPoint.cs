﻿namespace FluffySpoon.Automation.Web.Fluent.Expect.Root
{
	class ExpectMethodChainEntryPoint : ExpectMethodChainRoot<IBaseMethodChainNode>
	{
		public ExpectMethodChainEntryPoint()
		{

		}

		public override IBaseMethodChainNode Clone()
		{
			return new ExpectMethodChainEntryPoint();
		}
	}
}
