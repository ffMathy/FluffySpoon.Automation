using Microsoft.Extensions.DependencyInjection;
using FluffySpoon.Automation.Web.JQuery;
using FluffySpoon.Automation.Web.Puppeteer;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace FluffySpoon.Automation.Web.Tests
{
	[TestClass]
	public class WebAutomationEngineTests
	{
		private class TestCase
		{
			public Action<IServiceCollection> EngineConfiguration { get; }

			public TestCase(
				Action<IServiceCollection> selectorConfiguration)
			{
				EngineConfiguration = selectorConfiguration;
			}
		}

		[TestMethod]
		public async Task WebAutomationEngineTest()
		{
			var testCases = new List<TestCase>
			{
				new TestCase(p => p.AddPuppeteerWebAutomationFrameworkInstance(AutomationEngineFactory.GetPuppeteerDriverAsync))
			};

			using (var server = WebServerHelper.CreateWebServer())
			{
				foreach (var testCase in testCases)
				{
					var serviceCollection = new ServiceCollection();
					testCase.EngineConfiguration(serviceCollection);

					serviceCollection.AddJQueryDomSelector();

					var serviceProvider = serviceCollection.BuildServiceProvider();
					
					await RunTestAsync(
						serviceProvider,
						async x => {
							await x.OpenTest("engine/find.html");

							var aMatches = await x.Find(".a");
							var bMatches = await x.Find(".b");
							var cMatches = await x.Find(".c");

							Assert.AreEqual(6, aMatches.Count);
							Assert.AreEqual(3, bMatches.Count);
							Assert.AreEqual(2, cMatches.Count);
						});
				}
			}
		}

		private async Task RunTestAsync(IServiceProvider serviceProvider, Func<IWebAutomationEngine, Task> run)
		{
			using (var automationEngine = serviceProvider.GetRequiredService<IWebAutomationEngine>())
			{
				await run(automationEngine);
			}
		}
	}
}
