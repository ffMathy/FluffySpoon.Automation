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

				serviceCollection.AddSeleniumWebAutomationFrameworkInstance(GetFirefoxDriverAsync);
				serviceCollection.AddSeleniumWebAutomationFrameworkInstance(GetChromeDriverAsync);
				serviceCollection.AddSeleniumWebAutomationFrameworkInstance(GetEdgeDriverAsync);

				serviceCollection.AddPuppeteerWebAutomationFrameworkInstance(GetPuppeteerDriverAsync);

				var serviceProvider = serviceCollection.BuildServiceProvider();

				using (var automationEngine = serviceProvider.GetRequiredService<IWebAutomationEngine>())
				{
					await automationEngine.InitializeAsync();

					await automationEngine
						.Open("https://google.com");
					
					await automationEngine
						.Enter("this is a very long test that works").In("input[type=text]:visible")
						.Wait(until => 
							until.Exists("input[type=submit][name=btnK]:visible"));

					var elements = await automationEngine
						.Click.On("input[type=submit][name=btnK]:visible")
						.Wait(until => 
							until.Exists("#rso .g:visible"))
						.Expect
						.Count(10).Of("#rso .g:visible");

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
			await new BrowserFetcher().DownloadAsync(BrowserFetcher.DefaultRevision);
			return await Puppeteer.LaunchAsync(new LaunchOptions
			{
				Headless = false,
				DefaultViewport = new ViewPortOptions() {
					Width = 1100,
					Height = 500
				}
			});
		}

		private static async Task<IWebDriver> GetEdgeDriverAsync()
		{
			var options = new EdgeOptions() {
				AcceptInsecureCertificates = true,
				UnhandledPromptBehavior = UnhandledPromptBehavior.Accept,
				PageLoadStrategy = PageLoadStrategy.Eager
			};

			var driver = new EdgeDriver(Environment.CurrentDirectory, options);
			return driver;
		}

		private static async Task<IWebDriver> GetFirefoxDriverAsync()
		{
			var options = new FirefoxOptions() {
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

			var options = new ChromeOptions() {
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
