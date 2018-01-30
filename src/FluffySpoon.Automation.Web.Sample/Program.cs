using Microsoft.Extensions.DependencyInjection;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System;
using System.Threading.Tasks;
using FluffySpoon.Automation.Web.JQuery;
using FluffySpoon.Automation.Web.Selenium;

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
				serviceCollection.AddSeleniumWebAutomationFrameworkInstance(() => GetChromeDriver());

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

		private static ChromeDriver GetChromeDriver()
		{
			var service = ChromeDriverService.CreateDefaultService(Environment.CurrentDirectory);
			service.EnableVerboseLogging = false;
			service.HideCommandPromptWindow = true;
			service.SuppressInitialDiagnosticInformation = true;

			var options = new ChromeOptions() {
				Proxy = null,
				PageLoadStrategy = PageLoadStrategy.Normal,
				UnhandledPromptBehavior = UnhandledPromptBehavior.Accept
			};
			//options.AddArgument("--headless");

			var chromeDriver = new ChromeDriver(Environment.CurrentDirectory, options);
			chromeDriver.Manage().Timeouts().ImplicitWait = TimeSpan.Zero;

			return chromeDriver;
		}
	}
}
