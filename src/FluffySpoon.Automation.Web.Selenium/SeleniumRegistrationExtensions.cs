using System;
using System.Threading.Tasks;
using FluffySpoon.Automation.Web.Dom;
using Microsoft.Extensions.DependencyInjection;
using OpenQA.Selenium;

namespace FluffySpoon.Automation.Web.Selenium
{
	public static class SeleniumRegistrationExtensions
	{
		public static void AddSeleniumWebAutomationFrameworkInstance(this ServiceCollection services, Func<Task<IWebDriver>> driverConstructor)
		{
			RegistrationExtensions.AddWebAutomationFrameworkInstance(provider => 
				new SeleniumWebAutomationFrameworkInstance(
					driverConstructor,
					provider.GetRequiredService<IDomTunnel>()));
		}
	}
}
