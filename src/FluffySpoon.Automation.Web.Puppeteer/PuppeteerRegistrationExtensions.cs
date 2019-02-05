﻿using System;
using System.Threading.Tasks;
using FluffySpoon.Automation.Web.Dom;
using Microsoft.Extensions.DependencyInjection;
using PuppeteerSharp;

namespace FluffySpoon.Automation.Web.Selenium
{
	public static class PuppeteerRegistrationExtensions
	{
		public static void AddPuppeteerWebAutomationFrameworkInstance(this ServiceCollection services, Func<Task<Browser>> driverConstructor)
		{
			RegistrationExtensions.AddWebAutomationFrameworkInstance(provider => 
				new PuppeteerWebAutomationFrameworkInstance(
					driverConstructor,
					provider.GetRequiredService<IDomTunnel>()));
		}
	}
}
