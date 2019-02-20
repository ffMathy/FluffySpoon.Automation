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

		protected override bool MayCauseElementSideEffects => false;

		public override IReadOnlyList<IDomElement> Elements
		{
			get => Parent.Elements;
		}

		public ITakeScreenshotOfTargetSaveAsMethodChainNode SaveAs(string jpegFileName)
		{
			return MethodChainContext.Enqueue(new TakeScreenshotOfTargetSaveAsMethodChainNode(jpegFileName));
		}

		protected override async Task OnExecuteAsync(IWebAutomationFrameworkInstance framework)
		{
			var screenshots = new List<SKBitmap>();
			using (var bodyScreenshot = await framework.TakeScreenshotAsync())
			{
				foreach (var element in Elements)
				{
					var elementBounds = element.BoundingClientRectangle;
					if (elementBounds.Width == 0 || elementBounds.Height == 0)
						throw new InvalidOperationException("Can't take a screenshot of a zero-pixel width or zero-pixel height element.");

					var elementScreenshot = new SKBitmap(
						(int)Math.Round(elementBounds.Width),
						(int)Math.Round(elementBounds.Height));

					bodyScreenshot.ExtractSubset(
						elementScreenshot,
						new SKRectI()
						{
							Location = new SKPointI(
								(int)Math.Round(elementBounds.Left),
								(int)Math.Round(elementBounds.Top)),
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

		public override IBaseMethodChainNode Clone()
		{
			return new TakeScreenshotOfTargetMethodChainNode();
		}
	}
}