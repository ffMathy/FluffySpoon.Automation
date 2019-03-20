using FluffySpoon.Automation.Web.Fluent.Root;
using SkiaSharp;
using System;
using System.IO;
using System.Threading.Tasks;

namespace FluffySpoon.Automation.Web.Fluent.TakeScreenshot
{
    public delegate string GetScreenshotFilePathDelegate(IWebAutomationFrameworkInstance framework, int elementIndex);

	class TakeScreenshotOfTargetSaveAsMethodChainNode : MethodChainRoot<TakeScreenshotOfTargetMethodChainNode>, ITakeScreenshotOfTargetSaveAsMethodChainNode
	{
		private readonly GetScreenshotFilePathDelegate _jpegFilePath;

		protected override bool MayCauseElementSideEffects => false;

		public TakeScreenshotOfTargetSaveAsMethodChainNode(GetScreenshotFilePathDelegate jpegFileName)
		{
			_jpegFilePath = jpegFileName;
		}

		protected override async Task OnExecuteAsync(IWebAutomationFrameworkInstance framework)
		{
			var fileOffset = -1;

			foreach (var screenshotBitmap in Parent.Screenshots)
			{
				++fileOffset;

                var fileName = _jpegFilePath(framework, fileOffset);

				using (screenshotBitmap)
				using (var screenshotImage = SKImage.FromBitmap(screenshotBitmap))
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
			return new TakeScreenshotOfTargetSaveAsMethodChainNode(_jpegFilePath);
		}
	}
}