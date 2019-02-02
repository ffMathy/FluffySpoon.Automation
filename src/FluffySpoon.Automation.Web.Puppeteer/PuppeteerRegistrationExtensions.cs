using System;
using Microsoft.Extensions.DependencyInjection;

namespace FluffySpoon.Automation.Web.Selenium
{
	public static class PuppeteerRegistrationExtensions
	{
		public static void AddSeleniumWebAutomationFrameworkInstance(this ServiceCollection services, Func<IWebDriver> driverConstructor)
		{
			RegistrationExtensions.AddWebAutomationFrameworkInstance(provider => 
				new SeleniumWebAutomationFrameworkInstance(
					provider.GetRequiredService<IDomSelectorStrategy>(), 
					driverConstructor()));
		}
	}
}
