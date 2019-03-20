using FluffySpoon.Automation.Web.Dom;
using FluffySpoon.Automation.Web.Fluent.Root;
using SkiaSharp;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace FluffySpoon.Automation.Web.Fluent.TakeScreenshot
{
	class TakeScreenshotOfTargetsMethodChainNode : MethodChainRoot<TakeScreenshotChainNode>, ITakeScreenshotOfTargetsMethodChainNode
	{
		internal IReadOnlyList<(SKBitmap bitmap, IDomElement element)> Screenshots { get; private set; }

		protected override bool MayCauseElementSideEffects => false;

		public override IReadOnlyList<IDomElement> Elements
		{
			get => Parent.Elements; protected internal set => Parent.Elements = value;
		}

		public ITakeScreenshotOfTargetsSaveAsMethodChainNode SaveAs(GetScreenshotFilePathDelegate jpegFilePathSelector)
		{
			return MethodChainContext.Enqueue(new TakeScreenshotOfTargetsSaveAsMethodChainNode(jpegFilePathSelector));
		}

		protected override async Task OnExecuteAsync(IWebAutomationFrameworkInstance framework)
		{
			var screenshots = new List<(SKBitmap bitmap, IDomElement element)>();
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

					screenshots.Add((elementScreenshot, element));
				}
			}

			Screenshots = screenshots;

			await base.OnExecuteAsync(framework);
		}

		public override IBaseMethodChainNode Clone()
		{
			var node = new TakeScreenshotOfTargetsMethodChainNode();
            return node;
		}
	}
}