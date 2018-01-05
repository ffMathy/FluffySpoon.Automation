using FluffySpoon.Automation.Web.Dom;
using FluffySpoon.Automation.Web.JQuery;
using Microsoft.Extensions.DependencyInjection;

namespace FluffySpoon.Automation.Web
{
	public static class JQueryRegistrationExtensions
	{
		public static void UseJQueryDomSelector(this ServiceCollection services)
		{
			services.UseFluffySpoonAutomationWeb();
			services.AddTransient<IDomSelectorStrategyFactory, JQueryDomSelectorStrategyFactory>();
		}
	}
}
