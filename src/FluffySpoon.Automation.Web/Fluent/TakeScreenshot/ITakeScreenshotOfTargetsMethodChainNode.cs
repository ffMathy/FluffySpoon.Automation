using FluffySpoon.Automation.Web.Fluent.Root;

namespace FluffySpoon.Automation.Web.Fluent.TakeScreenshot
{
	public interface ITakeScreenshotOfTargetsMethodChainNode: IBaseMethodChainNode
	{
		ITakeScreenshotOfTargetsSaveAsMethodChainNode SaveAs(GetScreenshotFilePathDelegate jpegFilePathSelector);
	}
}