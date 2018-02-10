﻿using FluffySpoon.Automation.Web.Fluent.Targets;

namespace FluffySpoon.Automation.Web.Fluent.TakeScreenshot
{
	class TakeScreenshotChainNode : BaseDomElementTargetsMethodChainNode<IBaseMethodChainNode, TakeScreenshotChainNode, TakeScreenshotOfTargetMethodChainNode>
	{
		public override IBaseMethodChainNode Clone()
		{
			var clone = new TakeScreenshotChainNode();
			TransferDelegation(clone);

			return clone;
		}
	}
}
