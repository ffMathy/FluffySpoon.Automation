using FluffySpoon.Automation.Web.Dom;
using FluffySpoon.Automation.Web.Selenium;
using Microsoft.Extensions.DependencyInjection;
using OpenQA.Selenium;

namespace FluffySpoon.Automation.Web
{
	public static class SeleniumRegistrationExtensions
	{
		public static void AddSeleniumWebAutomationFrameworkInstance(this ServiceCollection services, IWebDriver driver)
		{
			RegistrationExtensions.AddWebAutomationFrameworkInstance(provider => 
				new SeleniumWebAutomationFrameworkInstance(
					provider.GetRequiredService<IDomSelectorStrategyFactory>(), 
					provider.GetRequiredService<IDomElementFactory>(),
					driver));
		}
	}
}
