using FluffySpoon.Automation.Web.Dom;
using Microsoft.Extensions.DependencyInjection;

namespace FluffySpoon.Automation.Web.Css
{
	public static class CssRegistrationExtensions
	{
		public static void AddCssDomSelector(this IServiceCollection services)
		{
			services.AddFluffySpoonAutomationWeb();
			services.AddSingleton<IDomSelectorStrategy, CssSelectorStrategy>();
		}
	}
}
