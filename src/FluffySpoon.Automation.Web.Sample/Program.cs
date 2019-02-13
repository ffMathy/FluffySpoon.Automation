using Microsoft.Extensions.DependencyInjection;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System;
using System.Threading.Tasks;
using FluffySpoon.Automation.Web.JQuery;
using FluffySpoon.Automation.Web.Selenium;
using OpenQA.Selenium.Edge;
using OpenQA.Selenium.Firefox;
using PuppeteerSharp;
using System.Diagnostics;

namespace FluffySpoon.Automation.Web.Sample
{
	class Program
	{
		static async Task Main(string[] args)
		{
			try
			{
				var serviceCollection = new ServiceCollection();
				serviceCollection.UseJQueryDomSelector();

				//serviceCollection.AddSeleniumWebAutomationFrameworkInstance(GetFirefoxDriverAsync);
				//serviceCollection.AddSeleniumWebAutomationFrameworkInstance(GetChromeDriverAsync);
				//serviceCollection.AddSeleniumWebAutomationFrameworkInstance(GetEdgeDriverAsync);

				serviceCollection.AddPuppeteerWebAutomationFrameworkInstance(GetPuppeteerDriverAsync);

				var serviceProvider = serviceCollection.BuildServiceProvider();

				using (var automationEngine = serviceProvider.GetRequiredService<IWebAutomationEngine>())
				{
					await automationEngine.InitializeAsync();

					await automationEngine
						.Open("https://google.com");

					//await automationEngine
					//	.Drag.From("input:first").To("input:first");

					await automationEngine
						.Expect
						.Count(1337).Of("input[type=submit][name=btnK]:visible");

					//await automationEngine.Open("about:blank");

					//foreach (var element in elements)
					//{
					//	await automationEngine.TakeScreenshot.Of(element).SaveAs("screenshot.jpg");
					//}

					Console.WriteLine("Test done!");
				}
			}
			catch (Exception ex)
			{
				await Console.Error.WriteLineAsync(ex.ToString());
				Console.ReadLine();
			}
		}

		private static async Task<Browser> GetPuppeteerDriverAsync()
		{
			foreach (var process in Process.GetProcessesByName("chrome"))
				process.Kill();

			await new BrowserFetcher().DownloadAsync(BrowserFetcher.DefaultRevision);
			return await Puppeteer.LaunchAsync(new LaunchOptions
			{
				Headless = false,
				DefaultViewport = new ViewPortOptions()
				{
					Width = 1100,
					Height = 500
				}
			});
		}

		private static async Task<IWebDriver> GetEdgeDriverAsync()
		{
			var options = new EdgeOptions()
			{
				AcceptInsecureCertificates = true,
				UnhandledPromptBehavior = UnhandledPromptBehavior.Accept,
				PageLoadStrategy = PageLoadStrategy.Eager
			};

			var driver = new EdgeDriver(Environment.CurrentDirectory, options);
			return driver;
		}

		private static async Task<IWebDriver> GetFirefoxDriverAsync()
		{
			var options = new FirefoxOptions()
			{
				PageLoadStrategy = PageLoadStrategy.Eager,
				AcceptInsecureCertificates = true,
				UnhandledPromptBehavior = UnhandledPromptBehavior.Accept
			};

			var driver = new FirefoxDriver(Environment.CurrentDirectory, options);
			return driver;
		}

		private static async Task<IWebDriver> GetChromeDriverAsync()
		{
			var service = ChromeDriverService.CreateDefaultService(Environment.CurrentDirectory);
			service.EnableVerboseLogging = false;
			service.HideCommandPromptWindow = true;
			service.SuppressInitialDiagnosticInformation = true;

			var options = new ChromeOptions()
			{
				Proxy = null,
				UnhandledPromptBehavior = UnhandledPromptBehavior.Accept,
				AcceptInsecureCertificates = true
			};

			var chromeDriver = new ChromeDriver(Environment.CurrentDirectory, options);
			chromeDriver.Manage().Timeouts().ImplicitWait = TimeSpan.Zero;

			return chromeDriver;
		}
	}
}
