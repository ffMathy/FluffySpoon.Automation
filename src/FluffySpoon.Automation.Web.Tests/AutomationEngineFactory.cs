using System;
using System.IO;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Hosting.Server.Features;
using PuppeteerSharp;
using System.Linq;
using System.Threading.Tasks;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Edge;
using OpenQA.Selenium.Firefox;

namespace FluffySpoon.Automation.Web.Tests
{
	class AutomationEngineFactory
	{
		public static async Task<Browser> GetPuppeteerDriverAsync()
		{
			await new BrowserFetcher().DownloadAsync(BrowserFetcher.DefaultRevision);
			return await PuppeteerSharp.Puppeteer.LaunchAsync(new LaunchOptions
			{
				Headless = false,
				DefaultViewport = new ViewPortOptions()
				{
					Width = 1100,
					Height = 1000
				}
			});
		}

		public static Task<IWebDriver> GetEdgeDriverAsync()
		{
			var options = new EdgeOptions() {
                PageLoadStrategy = PageLoadStrategy.Eager,
                UnhandledPromptBehavior = UnhandledPromptBehavior.Accept,
                UseInPrivateBrowsing = true
            };

            var pathsToCheck = new[]
            {
                Path.Combine("C:", "Windows", "SysWOW64"),
                Path.Combine(
                    Environment.CurrentDirectory,
                    "Drivers")
            };

            var driverFileName = "MicrosoftWebDriver.exe";
            var pathToUse = pathsToCheck
                .First(x => File.Exists(
                    Path.Combine(x, driverFileName)));

			var service = EdgeDriverService.CreateDefaultService(
                pathToUse, 
                driverFileName, 
                52296);

            var driver = new EdgeDriver(service, options);
			return Task.FromResult<IWebDriver>(driver);
		}

		public static Task<IWebDriver> GetFirefoxDriverAsync()
		{
			var options = new FirefoxOptions()
			{
				PageLoadStrategy = PageLoadStrategy.Eager,
				AcceptInsecureCertificates = true,
				UnhandledPromptBehavior = UnhandledPromptBehavior.Accept
			};

            var service = FirefoxDriverService.CreateDefaultService(
                Path.Combine(
                    Environment.CurrentDirectory,
                    "Drivers"));

            service.Host = "127.0.0.1";
            service.HostName = "127.0.0.1";

            var driver = new FirefoxDriver(
                service,
				options);
			return Task.FromResult<IWebDriver>(driver);
		}

		public static Task<IWebDriver> GetChromeDriverAsync()
		{
			var service = ChromeDriverService.CreateDefaultService(
                Path.Combine(
                    Environment.CurrentDirectory,
                    "Drivers"));

			service.EnableVerboseLogging = false;
			service.HideCommandPromptWindow = true;
			service.SuppressInitialDiagnosticInformation = true;

            service.HostName = "127.0.0.1";

            var options = new ChromeOptions()
			{
				Proxy = null,
				UnhandledPromptBehavior = UnhandledPromptBehavior.Accept,
				AcceptInsecureCertificates = true
			};

			var chromeDriver = new ChromeDriver(
                service,
				options);
			chromeDriver.Manage().Timeouts().ImplicitWait = TimeSpan.Zero;

			return Task.FromResult<IWebDriver>(chromeDriver);
		}
	}
}
