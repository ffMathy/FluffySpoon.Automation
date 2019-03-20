using FluffySpoon.Automation.Web.JQuery;
using FluffySpoon.Automation.Web.Puppeteer;
using FluffySpoon.Automation.Web.Css;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FluffySpoon.Automation.Web.Tests;

namespace FluffySpoon.Automation.Web
{
	[TestClass]
	public class SelectorTests
	{
		private class TestCase
		{
			public Action<IServiceCollection> SelectorConfiguration { get; }

			public string OuterElementSelector { get; }

			public string InnerElementSelector { get; }

			public TestCase(
				Action<IServiceCollection> selectorConfiguration,
				string outerElementSelector,
				string innerElementSelector)
			{
				SelectorConfiguration = selectorConfiguration;
				OuterElementSelector = outerElementSelector;
				InnerElementSelector = innerElementSelector;
			}
		}

		[TestMethod]
		public async Task SelectorTest()
		{
			var testCases = new List<TestCase>
			{
				new TestCase(
					p => p.AddJQueryDomSelector(),
					"#outer:first",
					".inner:first"),
                new TestCase(
                    p => p.AddCssDomSelector(),
                    "#outer",
                    ".inner")
            };

			using (var server = WebServerHelper.CreateWebServer())
			{
				foreach (var testCase in testCases)
				{
					var serviceCollection = new ServiceCollection();
					testCase.SelectorConfiguration(serviceCollection);

					serviceCollection.AddPuppeteerWebAutomationFrameworkInstance(AutomationEngineFactory.GetPuppeteerDriverAsync);

					var serviceProvider = serviceCollection.BuildServiceProvider();
					using (var automationEngine = serviceProvider.GetRequiredService<IWebAutomationEngine>())
					{
						await automationEngine.InitializeAsync();

						await automationEngine.OpenTest(server, "selector/index.html");

						var outerElements = await automationEngine.Find(testCase.OuterElementSelector);
						Assert.IsNotNull(outerElements);

						var outerElement = outerElements.SingleOrDefault();
						Assert.IsNotNull(outerElement);
						Assert.AreEqual("hello world", outerElement.TextContent);

						var innerElements = await automationEngine.Find(testCase.InnerElementSelector);
						Assert.IsNotNull(innerElements);

						var innerElement = innerElements.SingleOrDefault();
						Assert.IsNotNull(innerElement);
						Assert.AreEqual("world", innerElement.TextContent?.Trim());
					}
				}
			}
		}
	}
}
