using Microsoft.Extensions.DependencyInjection;
using FluffySpoon.Automation.Web.JQuery;
using FluffySpoon.Automation.Web.Puppeteer;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using FluffySpoon.Automation.Web.Selenium;

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
				new TestCase(p => p.AddSeleniumWebAutomationFrameworkInstance(AutomationEngineFactory.GetEdgeDriverAsync)),
				new TestCase(p => p.AddSeleniumWebAutomationFrameworkInstance(AutomationEngineFactory.GetFirefoxDriverAsync)),
				new TestCase(p => p.AddPuppeteerWebAutomationFrameworkInstance(AutomationEngineFactory.GetPuppeteerDriverAsync)),
				new TestCase(p => p.AddSeleniumWebAutomationFrameworkInstance(AutomationEngineFactory.GetChromeDriverAsync)),
				new TestCase(p => p.AddSeleniumWebAutomationFrameworkInstance(AutomationEngineFactory.GetEdgeDriverAsync))
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
						async engine =>
						{
							await engine.OpenTest(server, "engine/hover.html");

							await engine.Hover.On("div");

							await engine.Expect
								.Text("hover").In("label");
						});

					await RunTestAsync(
						serviceProvider,
						async engine =>
						{
							await engine.OpenTest(server, "engine/focus.html");

							await engine.Focus.On("input");

							await engine.Expect
								.Text("focused").In("label");
						});

					await RunTestAsync(
						serviceProvider,
						async engine =>
						{
							await engine.OpenTest(server, "engine/enter.html");

							await engine.Enter("foo").In("input");

							await engine.Expect
								.Text("-foo-").In("label");
						});

					await RunTestAsync(
						serviceProvider,
						async engine =>
						{
							await engine.OpenTest(server, "engine/select.html");

							await engine.Select.ByText("Bar").From("select");

							await engine.Expect
								.Text("2").In("label");
						});

					await RunTestAsync(
						serviceProvider,
						async engine =>
						{
							await engine.OpenTest(server, "engine/select.html");

							await engine.Select.ByIndex(1).From("select");

							await engine.Expect
								.Text("2").In("label");
						});

					await RunTestAsync(
						serviceProvider,
						async engine =>
						{
							await engine.OpenTest(server, "engine/select.html");

							await engine.Select.ByValue("2").From("select");

							await engine.Expect
								.Text("2").In("label");
						});

					await RunTestAsync(
						serviceProvider,
						async engine =>
						{
							await engine.OpenTest(server, "engine/select.html");

							await engine.Select.ByValue(2).From("select");

							await engine.Expect
								.Text("2").In("label");
						});

					await RunTestAsync(
						serviceProvider,
						async engine =>
						{
							await engine.OpenTest(server, "engine/select.html");

							await engine.Select.ByValue((object)2).From("select");

							await engine.Expect
								.Text("2").In("label");
						});

					await RunTestAsync(
						serviceProvider,
						async engine =>
						{
							await engine.OpenTest(server, "engine/multi-select.html");

							await engine.Select.ByTexts("Bar", "Baz").From("select");

							await engine.Expect
								.Text("2, 3").In("label");
						});

					await RunTestAsync(
						serviceProvider,
						async engine =>
						{
							await engine.OpenTest(server, "engine/multi-select.html");

							await engine.Select.ByIndices(1, 2).From("select");

							await engine.Expect
								.Text("2, 3").In("label");
						});

					await RunTestAsync(
						serviceProvider,
						async engine =>
						{
							await engine.OpenTest(server, "engine/multi-select.html");

							await engine.Select.ByValues("2", "3").From("select");

							await engine.Expect
								.Text("2, 3").In("label");
						});

					await RunTestAsync(
						serviceProvider,
						async engine =>
						{
							await engine.OpenTest(server, "engine/multi-select.html");

							await engine.Select.ByValues(2, 3).From("select");

							await engine.Expect
								.Text("2, 3").In("label");
						});

					await RunTestAsync(
						serviceProvider,
						async engine =>
						{
							await engine.OpenTest(server, "engine/multi-select.html");

							await engine.Select.ByValues((object)2, (object)3).From("select");

							await engine.Expect
								.Text("2, 3").In("label");
						});

					await RunTestAsync(
						serviceProvider,
						async engine =>
						{
							await engine.OpenTest(server, "engine/double-click.html");

							var clickedButtons = await engine.Click.On("button");
							var clickedButton = clickedButtons.Single();
							Assert.AreEqual("not", clickedButton.TextContent);

							var newClickedButtons = await engine.DoubleClick.On("button");
							var newClickedButton = newClickedButtons.Single();
							Assert.AreEqual("clicked", newClickedButton.TextContent);
						});

					await RunTestAsync(
						serviceProvider,
						async engine =>
						{
							await engine.OpenTest(server, "engine/right-click.html");

							var clickedButtons = await engine.Click.On("button");
							var clickedButton = clickedButtons.Single();
							Assert.AreEqual("not", clickedButton.TextContent);

							var newClickedButtons = await engine.RightClick.On("button");
							var newClickedButton = newClickedButtons.Single();
							Assert.AreEqual("clicked", newClickedButton.TextContent);
						});

					await RunTestAsync(
						serviceProvider,
						async engine =>
						{
							await engine.OpenTest(server, "engine/wait-until.html");

							await engine.Wait(until => until
								.Text("loaded").In("button"));
						});

					await RunTestAsync(
						serviceProvider,
						async engine =>
						{
							await engine.OpenTest(server, "engine/click.html");

							var clickedButtons = await engine.Click.On("button");
							var clickedButton = clickedButtons.Single();
							Assert.AreEqual("clicked", clickedButton.TextContent);
						});

					await RunTestAsync(
						serviceProvider,
						async x =>
						{
							await x.OpenTest(server, "engine/find.html");

							var aMatches = await x.Find(".a");
							var bMatches = await x.Find(".b");
							var cMatches = await x.Find(".c");

							Assert.AreEqual(6, aMatches.Count);
							Assert.AreEqual(3, bMatches.Count);
							Assert.AreEqual(2, cMatches.Count);
						});

					//await RunTestAsync(
					//	serviceProvider,
					//	async engine =>
					//	{
					//		await engine.OpenTest(server, "engine/drag.html");

					//		await engine.Drag.From(".draggable.a").To(".draggable.b");

					//		await engine.Expect
					//			.Text("draggable-a").In("#result");
					//	});
				}
			}
		}

		private async Task RunTestAsync(IServiceProvider serviceProvider, Func<IWebAutomationEngine, Task> run)
		{
			using (var automationEngine = serviceProvider.GetRequiredService<IWebAutomationEngine>())
			{
				await automationEngine.InitializeAsync();
				await run(automationEngine);
			}
		}
	}
}
