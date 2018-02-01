using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;

namespace FluffySpoon.Automation.Web
{
	public static class RegistrationExtensions
	{
		private static readonly HashSet<Func<IServiceProvider, IWebAutomationFrameworkInstance>> webAutomationFrameworkInstanceConstructors;

		static RegistrationExtensions()
		{
			webAutomationFrameworkInstanceConstructors = new HashSet<Func<IServiceProvider, IWebAutomationFrameworkInstance>>();
		}

		public static void AddWebAutomationFrameworkInstance(
			Func<IServiceProvider, IWebAutomationFrameworkInstance> instanceConstructor)
		{
			webAutomationFrameworkInstanceConstructors.Add(instanceConstructor);
		}

		public static void UseFluffySpoonAutomationWeb(
			this ServiceCollection services)
		{
			services.AddTransient<IWebAutomationEngine, WebAutomationEngine>();
			services.AddTransient(provider => webAutomationFrameworkInstanceConstructors
				.Select(constructor => constructor(provider))
				.ToArray());
		}
	}
}
