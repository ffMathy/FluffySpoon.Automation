using FluffySpoon.Automation.Web.Dom;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;

namespace FluffySpoon.Automation.Web
{
	public static class RegistrationExtensions
	{
		public static void AddFluffySpoonAutomationWeb(
			this IServiceCollection services)
		{
			services.AddTransient<IDomTunnel, DomTunnel>();
			services.AddTransient<IWebAutomationEngine, WebAutomationEngine>();
		}
	}
}
