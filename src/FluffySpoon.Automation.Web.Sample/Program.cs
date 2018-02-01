using Microsoft.Extensions.DependencyInjection;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System;
using System.Threading.Tasks;
using FluffySpoon.Automation.Web.JQuery;
using FluffySpoon.Automation.Web.Selenium;
using OpenQA.Selenium.Edge;
using OpenQA.Selenium.Firefox;

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
				serviceCollection.AddSeleniumWebAutomationFrameworkInstance(GetChromeDriver);
				//serviceCollection.AddSeleniumWebAutomationFrameworkInstance(GetFirefoxDriver);
				serviceCollection.AddSeleniumWebAutomationFrameworkInstance(GetEdgeDriver);

				var serviceProvider = serviceCollection.BuildServiceProvider();

				using (var automationEngine = serviceProvider.GetRequiredService<IWebAutomationEngine>())
				{
					await automationEngine.InitializeAsync();

					await automationEngine
						.Open("https://google.com");
					
					await automationEngine
						.Enter("foobar").In("input[type=text]")
						.Wait(until => until.Count(2).Of("#sbtc .lsb:visible"));

					await automationEngine
						.Click.On("#sbtc .lsb:first")
						.Wait(until => until.Exists("#rso .g:visible"))
						.Wait(TimeSpan.FromSeconds(1))
						.Expect
						.Count(8).Of("#rso .g");
					
					await automationEngine
						.TakeScreenshot.Of(".srg .g").SaveAs(@"bin\result-picture.jpg");
				}

				Console.WriteLine("Test done!");
			}
			catch (Exception ex)
			{
				await Console.Error.WriteLineAsync(ex.ToString());
				Console.ReadLine();
			}
		}

		private static EdgeDriver GetEdgeDriver()
		{
			var options = new EdgeOptions() {
				AcceptInsecureCertificates = true,
				UnhandledPromptBehavior = UnhandledPromptBehavior.Accept,
				PageLoadStrategy = PageLoadStrategy.Eager
			};

			var driver = new EdgeDriver(Environment.CurrentDirectory, options);
			return driver;
		}

		private static FirefoxDriver GetFirefoxDriver()
		{
			var options = new FirefoxOptions() {
				PageLoadStrategy = PageLoadStrategy.Eager,
				AcceptInsecureCertificates = true,
				UnhandledPromptBehavior = UnhandledPromptBehavior.Accept
			};

			var driver = new FirefoxDriver(Environment.CurrentDirectory, options);
			return driver;
		}

		private static ChromeDriver GetChromeDriver()
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
