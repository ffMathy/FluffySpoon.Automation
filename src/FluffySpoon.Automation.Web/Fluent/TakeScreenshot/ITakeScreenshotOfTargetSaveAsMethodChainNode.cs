using System.Collections.Generic;
using FluffySpoon.Automation.Web.Dom;

namespace FluffySpoon.Automation.Web.Fluent.TakeScreenshot
{
	public interface ITakeScreenshotOfTargetSaveAsMethodChainNode: IBaseMethodChainNode, IAwaitable<IReadOnlyList<IDomElement>>
	{
	}
}