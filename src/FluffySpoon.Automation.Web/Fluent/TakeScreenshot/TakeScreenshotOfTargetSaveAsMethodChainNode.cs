﻿using FluffySpoon.Automation.Web.Fluent.Root;
using SkiaSharp;
using System.IO;
using System.Threading.Tasks;

namespace FluffySpoon.Automation.Web.Fluent.TakeScreenshot
{
	class TakeScreenshotOfTargetSaveAsMethodChainNode : MethodChainRoot<TakeScreenshotOfTargetMethodChainNode>, ITakeScreenshotOfTargetSaveAsMethodChainNode
	{
		private readonly string _jpegFileName;

		public TakeScreenshotOfTargetSaveAsMethodChainNode(string jpegFileName)
		{
			_jpegFileName = jpegFileName;
		}

		protected override async Task OnExecuteAsync(IWebAutomationFrameworkInstance framework)
		{
			var fileOffset = -1;
			var fileNameWithoutExtension = 
				Path.GetDirectoryName(_jpegFileName) + 
				Path.DirectorySeparatorChar + 
				Path.GetFileNameWithoutExtension(_jpegFileName);

			foreach (var screenshotBitmap in Parent.Screenshots)
			{
				++fileOffset;

				var fileName = Parent.Screenshots.Count > 1 ?
					$"{fileNameWithoutExtension}-{fileOffset}.jpg" :
					_jpegFileName;

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
	}
}