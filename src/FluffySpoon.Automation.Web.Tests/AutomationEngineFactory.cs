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

		public static async Task<IWebDriver> GetEdgeDriverAsync()
		{
			var options = new EdgeOptions()
			{
				AcceptInsecureCertificates = true,
				UnhandledPromptBehavior = UnhandledPromptBehavior.Accept,
				PageLoadStrategy = PageLoadStrategy.Eager
			};

			var driver = new EdgeDriver(
				Path.Combine(
					Environment.CurrentDirectory, 
					"Drivers"),
				options);
			return driver;
		}

		public static async Task<IWebDriver> GetFirefoxDriverAsync()
		{
			var options = new FirefoxOptions()
			{
				PageLoadStrategy = PageLoadStrategy.Eager,
				AcceptInsecureCertificates = true,
				UnhandledPromptBehavior = UnhandledPromptBehavior.Accept
			};

			var driver = new FirefoxDriver(
				Path.Combine(
					Environment.CurrentDirectory,
					"Drivers"),
				options);
			return driver;
		}

		public static async Task<IWebDriver> GetChromeDriverAsync()
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

			var chromeDriver = new ChromeDriver(
				Path.Combine(
					Environment.CurrentDirectory,
					"Drivers"),
				options);
			chromeDriver.Manage().Timeouts().ImplicitWait = TimeSpan.Zero;

			return chromeDriver;
		}
	}
}
