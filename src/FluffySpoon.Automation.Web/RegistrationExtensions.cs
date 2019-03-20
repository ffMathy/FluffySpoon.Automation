using FluffySpoon.Automation.Web.Dom;
using Microsoft.Extensions.DependencyInjection;

namespace FluffySpoon.Automation.Web
{
	public static class RegistrationExtensions
	{
		public static void AddFluffySpoonAutomationWeb(
			this IServiceCollection services)
		{
			services.AddTransient<IJavaScriptTunnel, JavaScriptTunnel>();
			services.AddTransient<IWebAutomationEngine, WebAutomationEngine>();
		}
	}
}
