using Microsoft.Extensions.DependencyInjection;
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
				serviceCollection.AddSeleniumWebAutomationFrameworkInstance(new ChromeDriver(Environment.CurrentDirectory));

				var serviceProvider = serviceCollection.BuildServiceProvider();
				var automationEngine = serviceProvider.GetRequiredService<IWebAutomationEngine>();

				await automationEngine
					.Open("https://google.com")
					.Enter("foobar")
					.In("input[type=text]");
			} catch(Exception ex) {
				await Console.Error.WriteLineAsync(ex.ToString());
			}

			Console.ReadLine();
        }
    }
}
