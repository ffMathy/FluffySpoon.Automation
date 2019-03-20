using System;
using System.Threading.Tasks;
using FluffySpoon.Automation.Web.Dom;
using Microsoft.Extensions.DependencyInjection;
using PuppeteerSharp;

namespace FluffySpoon.Automation.Web.Puppeteer
{
	public static class PuppeteerRegistrationExtensions
	{
		public static void AddPuppeteerWebAutomationFrameworkInstance(this IServiceCollection services, Func<Task<Browser>> driverConstructor)
		{
			services.AddFluffySpoonAutomationWeb();
			services.AddTransient<IWebAutomationFrameworkInstance>(provider => 
				new PuppeteerWebAutomationFrameworkInstance(
					driverConstructor,
					provider.GetRequiredService<IJavaScriptTunnel>()));
		}
	}
}
