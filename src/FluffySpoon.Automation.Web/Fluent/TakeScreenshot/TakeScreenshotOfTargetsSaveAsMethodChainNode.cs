using FluffySpoon.Automation.Web.Dom;
using FluffySpoon.Automation.Web.Fluent.Root;
using SkiaSharp;
using System;
using System.IO;
using System.Threading.Tasks;

namespace FluffySpoon.Automation.Web.Fluent.TakeScreenshot
{
    public delegate string GetScreenshotFilePathDelegate(IWebAutomationFrameworkInstance framework, int elementIndex = 0, IDomElement element = null);

	class TakeScreenshotOfTargetsSaveAsMethodChainNode : MethodChainRoot<TakeScreenshotOfTargetsMethodChainNode>, ITakeScreenshotOfTargetsSaveAsMethodChainNode
	{
		private readonly GetScreenshotFilePathDelegate _jpegFilePath;

		protected override bool MayCauseElementSideEffects => false;

		public TakeScreenshotOfTargetsSaveAsMethodChainNode(GetScreenshotFilePathDelegate jpegFileName)
		{
			_jpegFilePath = jpegFileName;
		}

		protected override async Task OnExecuteAsync(IWebAutomationFrameworkInstance framework)
		{
			var fileOffset = -1;

			foreach (var screenshot in Parent.Screenshots)
			{
				++fileOffset;

                var fileName = _jpegFilePath(framework, fileOffset, screenshot.element);

				using (screenshot.bitmap)
				using (var screenshotImage = SKImage.FromBitmap(screenshot.bitmap))
				using (var screenshotBytes = screenshotImage.Encode(SKEncodedImageFormat.Jpeg, 100))
				using (var fileStream = File.OpenWrite(fileName))
				{
					screenshotBytes.SaveTo(fileStream);
				}
			}

			await base.OnExecuteAsync(framework);
		}

		public override IBaseMethodChainNode Clone()
		{
			var node = new TakeScreenshotOfTargetsSaveAsMethodChainNode(_jpegFilePath);
            return node;
		}
	}
}