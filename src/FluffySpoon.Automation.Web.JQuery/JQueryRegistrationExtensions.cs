using FluffySpoon.Automation.Web.Dom;
using Microsoft.Extensions.DependencyInjection;

namespace FluffySpoon.Automation.Web.JQuery
{
	public static class JQueryRegistrationExtensions
	{
		public static void AddJQueryDomSelector(this IServiceCollection services)
		{
			services.AddFluffySpoonAutomationWeb();
			services.AddSingleton<IDomSelectorStrategy, JQueryDomSelectorStrategy>();
		}
	}
}
