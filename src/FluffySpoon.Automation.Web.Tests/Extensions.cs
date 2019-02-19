using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace FluffySpoon.Automation.Web.Tests
{
	public static class Extensions
	{
		public static async Task OpenTest(this IWebAutomationEngine engine, string page)
		{
			await engine.Open(WebServerHelper.Url + "/" + page);
		}
	}
}
