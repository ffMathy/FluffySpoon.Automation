using PuppeteerSharp;
using System.Diagnostics;
using System.Threading.Tasks;

namespace FluffySpoon.Automation.Web.Tests
{
	class AutomationEngineFactory
	{
		public static async Task<Browser> GetPuppeteerDriverAsync()
		{
			foreach (var process in Process.GetProcessesByName("chrome"))
				process.Kill();

			await new BrowserFetcher().DownloadAsync(BrowserFetcher.DefaultRevision);
			return await PuppeteerSharp.Puppeteer.LaunchAsync(new LaunchOptions
			{
				Headless = false,
				DefaultViewport = new ViewPortOptions()
				{
					Width = 1100,
					Height = 500
				}
			});
		}
	}
}
