using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Hosting.Server.Features;
using PuppeteerSharp;
using System.Linq;
using System.Threading.Tasks;

namespace FluffySpoon.Automation.Web.Tests
{
	class AutomationEngineFactory
	{
		public static async Task<Browser> GetPuppeteerDriverAsync()
		{
			await new BrowserFetcher().DownloadAsync(BrowserFetcher.DefaultRevision);
			return await PuppeteerSharp.Puppeteer.LaunchAsync(new LaunchOptions
			{
				Headless = true,
				DefaultViewport = new ViewPortOptions()
				{
					Width = 1100,
					Height = 500
				}
			});
		}
	}
}
