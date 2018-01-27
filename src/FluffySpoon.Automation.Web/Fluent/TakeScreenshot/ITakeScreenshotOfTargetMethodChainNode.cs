using FluffySpoon.Automation.Web.Fluent.Root;

namespace FluffySpoon.Automation.Web.Fluent.TakeScreenshot
{
	public interface ITakeScreenshotOfTargetMethodChainNode: IBaseMethodChainNode
	{
		IMethodChainRoot SaveAs(string jpegFileName);
	}
}