using FluffySpoon.Automation.Web.Dom;
using Microsoft.Extensions.DependencyInjection;

namespace FluffySpoon.Automation.Web.JQuery
{
	public static class JQueryRegistrationExtensions
	{
		public static void UseJQueryDomSelector(this ServiceCollection services)
		{
			services.UseFluffySpoonAutomationWeb();
			services.AddSingleton<IDomSelectorStrategy, JQueryDomSelectorStrategy>();
		}
	}
}
