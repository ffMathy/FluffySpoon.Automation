﻿using System;
using System.Collections.Generic;
using System.Text;

namespace FluffySpoon.Automation.Web.Fluent.Expect.Root
{
	class ExpectMethodChainEntryPoint : ExpectMethodChainRoot<IBaseMethodChainNode>
	{
		public override IBaseMethodChainNode Clone()
		{
			return this;
		}
	}
}
