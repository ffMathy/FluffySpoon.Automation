using FluffySpoon.Automation.Web.Dom;
using FluffySpoon.Automation.Web.Fluent;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FluffySpoon.Automation.Web
{
	public static class RegistrationExtensions
	{
		private static readonly HashSet<Func<IServiceProvider, IWebAutomationFrameworkInstance>> WebAutomationFrameworkInstanceConstructors;

		static RegistrationExtensions()
		{
			WebAutomationFrameworkInstanceConstructors = new HashSet<Func<IServiceProvider, IWebAutomationFrameworkInstance>>();
		}

		public static void AddWebAutomationFrameworkInstance(
			Func<IServiceProvider, IWebAutomationFrameworkInstance> instanceConstructor)
		{
			WebAutomationFrameworkInstanceConstructors.Add(instanceConstructor);
		}

		public static void UseFluffySpoonAutomationWeb(
			this ServiceCollection services)
		{
			services.AddTransient<IWebAutomationEngine, WebAutomationEngine>();
			services.AddTransient<IDomElementFactory, DomElementFactory>();
			services.AddTransient<IMethodChainContextFactory, MethodChainContextFactory>();

			services.AddTransient(provider => WebAutomationFrameworkInstanceConstructors.Select(constructor => constructor(provider)));
		}
	}
}
