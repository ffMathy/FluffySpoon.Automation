using System;
using System.Threading.Tasks;
using FluffySpoon.Automation.Web.Dom;
using Microsoft.Extensions.DependencyInjection;
using OpenQA.Selenium;

namespace FluffySpoon.Automation.Web.Selenium
{
	public static class SeleniumRegistrationExtensions
	{
		public static void AddSeleniumWebAutomationFrameworkInstance(this IServiceCollection services, Func<Task<IWebDriver>> driverConstructor)
		{
			services.AddTransient<IWebAutomationFrameworkInstance>(provider => 
				new SeleniumWebAutomationFrameworkInstance(
					driverConstructor,
					provider.GetRequiredService<IJavaScriptTunnel>()));
		}
	}
}
