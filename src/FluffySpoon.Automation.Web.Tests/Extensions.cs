using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Hosting.Server.Features;
using System.Linq;
using System.Threading.Tasks;

namespace FluffySpoon.Automation.Web.Tests
{
	public static class Extensions
	{
		public static async Task OpenTest(this IWebAutomationEngine engine, IWebHost server, string page)
		{
			var address = server.ServerFeatures
				.Get<IServerAddressesFeature>()
				.Addresses
				.Single();
			await engine.Open(address + "/" + page);
		}
	}
}
