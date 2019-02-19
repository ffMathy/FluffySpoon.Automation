using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.FileProviders;
using System.IO;

namespace FluffySpoon.Automation.Web.Tests
{
	class WebServerHelper
	{
		public const string Url = "http://localhost:56712";

		public static IWebHost CreateWebServer()
		{
			var path = Path.Combine(
				Directory.GetCurrentDirectory(),
				"wwwroot");
			var server = WebHost.CreateDefaultBuilder()
				.UseUrls(Url)
				.Configure(a => a
					.UseStaticFiles(new StaticFileOptions()
					{
						ServeUnknownFileTypes = true,
						DefaultContentType = "text/html",
						FileProvider = new PhysicalFileProvider(path)
					}))
				.Build();

			server.Start();
			return server;
		}
	}
}
