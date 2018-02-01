using FluffySpoon.Automation.Web.Dom;
using FluffySpoon.Automation.Web.Fluent.Root;
using SkiaSharp;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FluffySpoon.Automation.Web.Fluent.TakeScreenshot
{
	class TakeScreenshotOfTargetMethodChainNode : MethodChainRoot<TakeScreenshotChainNode>, ITakeScreenshotOfTargetMethodChainNode
	{
		internal IReadOnlyList<SKBitmap> Screenshots { get; private set; }

		public override IReadOnlyList<IDomElement> Elements
		{
			get => Parent.Elements;
		}

		public ITakeScreenshotOfTargetSaveAsMethodChainNode SaveAs(string jpegFileName)
		{
			return MethodChainContext.Enqueue(() => new TakeScreenshotOfTargetSaveAsMethodChainNode(jpegFileName));
		}

		protected override async Task OnExecuteAsync(IWebAutomationFrameworkInstance framework)
		{
			var screenshots = new List<SKBitmap>();
			using (var bodyScreenshot = await framework.TakeScreenshotAsync())
			{
				foreach (var element in Elements)
				{
					var elementBounds = element.BoundingClientRectangle;

					var elementScreenshot = new SKBitmap(
						(int)Math.Round(elementBounds.Width),
						(int)Math.Round(elementBounds.Height));

					bodyScreenshot.ExtractSubset(
						elementScreenshot,
						new SKRectI()
						{
							Location = new SKPointI(
								(int)Math.Round(elementBounds.X),
								(int)Math.Round(elementBounds.Y)),
							Size = new SKSizeI(
								elementScreenshot.Width,
								elementScreenshot.Height)
						});

					screenshots.Add(elementScreenshot);
				}
			}

			Screenshots = screenshots;

			await base.OnExecuteAsync(framework);
		}
	}
}