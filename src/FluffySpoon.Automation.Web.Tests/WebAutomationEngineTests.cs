using Microsoft.Extensions.DependencyInjection;
using FluffySpoon.Automation.Web.JQuery;
using FluffySpoon.Automation.Web.Puppeteer;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using FluffySpoon.Automation.Web.Selenium;
using Microsoft.AspNetCore.Hosting;
using FluffySpoon.Automation.Web.Exceptions;
using System.Diagnostics;

namespace FluffySpoon.Automation.Web.Tests
{
	[TestClass]
	public class WebAutomationEngineTests
	{
		private class TestCase
		{
            public Action<IServiceCollection> EngineConfiguration { get; }

            public DriverType DriverType { get; }

            public TestCase(
                DriverType driverType,
				Action<IServiceCollection> selectorConfiguration)
			{
                DriverType = driverType;
                EngineConfiguration = selectorConfiguration;
			}
		}

        private enum DriverType
        {
            SeleniumEdge,
            SeleniumFirefox,
            SeleniumChrome,
            Puppeteer
        }

		[TestMethod]
		public async Task WebAutomationEngineTest()
		{
			var testCases = new List<TestCase>
            {
                new TestCase(
                    DriverType.SeleniumChrome,
                    p => p.AddSeleniumWebAutomationFrameworkInstance(AutomationEngineFactory.GetChromeDriverAsync)),
                new TestCase(
                    DriverType.Puppeteer,
                    p => p.AddPuppeteerWebAutomationFrameworkInstance(AutomationEngineFactory.GetPuppeteerDriverAsync)),
                new TestCase(
                    DriverType.SeleniumFirefox,
                    p => p.AddSeleniumWebAutomationFrameworkInstance(AutomationEngineFactory.GetFirefoxDriverAsync)),
                new TestCase(
                    DriverType.SeleniumEdge,
                    p => p.AddSeleniumWebAutomationFrameworkInstance(AutomationEngineFactory.GetEdgeDriverAsync)),
            };

			using (var server = WebServerHelper.CreateWebServer())
			{
				foreach (var testCase in testCases)
				{
					var serviceCollection = new ServiceCollection();
					testCase.EngineConfiguration(serviceCollection);

					serviceCollection.AddJQueryDomSelector();

					var serviceProvider = serviceCollection.BuildServiceProvider();

                    //HACK: commented out because Selenium won't work with drag and drop.
                    //https://stackoverflow.com/questions/29381233/how-to-simulate-html5-drag-and-drop-in-selenium-webdriver/29381532
                    if (testCase.DriverType == DriverType.Puppeteer)
                    {
                        //Commented out until Puppeteer stops freezing until mouse move

                        //await RunTestAsync(
                        //    testCase.DriverType,
                        //    serviceProvider,
                        //    async engine =>
                        //    {
                        //        await engine.OpenTest(server, "engine/drag.html");

                        //        await engine.Drag.From(".draggable.a").To(".draggable.b");

                        //        await engine.Expect
                        //            .Text("draggable-a").In("#result");
                        //    });
                    }

                    await RunTestAsync(
                        testCase.DriverType,
                        serviceProvider,
                        async engine =>
                        {
                            await engine.OpenTest(server, "engine/focus.html");

                            await engine.Focus.On("input");

                            await engine.Expect
                                .Text("focused").In("label");
                        });

                    await RunTestAsync(
                        testCase.DriverType,
                        serviceProvider,
                        async engine =>
                        {
                            await engine.OpenTest(server, "engine/hover.html");

                            await engine.Hover.On("div");

                            await engine.Expect
                                .Text("hover").In("label");
                        });

                    //Selenium webdriver doesn't support multi-select properly: https://developer.microsoft.com/en-us/microsoft-edge/platform/issues/20638074/
                    if (testCase.DriverType != DriverType.SeleniumEdge)
                    {
                        await RunTestAsync(
                            testCase.DriverType,
                            serviceProvider,
                            async engine =>
                            {
                                await engine.OpenTest(server, "engine/multi-select.html");

                                await engine.Select.ByTexts("Bar", "Baz").From("select");

                                await engine.Expect
                                    .Text("2, 3").In("label");
                            });

                        await RunTestAsync(
                            testCase.DriverType,
                            serviceProvider,
                            async engine =>
                            {
                                await engine.OpenTest(server, "engine/multi-select.html");

                                await engine.Select.ByIndices(1, 2).From("select");

                                await engine.Expect
                                    .Text("2, 3").In("label");
                            });

                        await RunTestAsync(
                            testCase.DriverType,
                            serviceProvider,
                            async engine =>
                            {
                                await engine.OpenTest(server, "engine/multi-select.html");

                                await engine.Select.ByValues("2", "3").From("select");

                                await engine.Expect
                                    .Text("2, 3").In("label");
                            });

                        await RunTestAsync(
                            testCase.DriverType,
                            serviceProvider,
                            async engine =>
                            {
                                await engine.OpenTest(server, "engine/multi-select.html");

                                await engine.Select.ByValues(2, 3).From("select");

                                await engine.Expect
                                    .Text("2, 3").In("label");
                            });

                        await RunTestAsync(
                            testCase.DriverType,
                            serviceProvider,
                            async engine =>
                            {
                                await engine.OpenTest(server, "engine/multi-select.html");

                                await engine.Select.ByValues((object)2, (object)3).From("select");

                                await engine.Expect
                                    .Text("2, 3").In("label");
                            });
                    }

					await RunTestAsync(
                        testCase.DriverType,
                        serviceProvider,
						async engine =>
						{
							await engine.OpenTest(server, "engine/enter.html");

							await engine.Enter("foo").In("input");

							await engine.Expect
								.Text("-foo-").In("label");
						});

					await RunTestAsync(
                        testCase.DriverType,
                        serviceProvider,
						async engine =>
						{
							await engine.OpenTest(server, "engine/select.html");

							await engine.Select.ByText("Bar").From("select");

							await engine.Expect
								.Text("2").In("label");
						});

					await RunTestAsync(
                        testCase.DriverType,
                        serviceProvider,
						async engine =>
						{
							await engine.OpenTest(server, "engine/select.html");

							await engine.Select.ByIndex(1).From("select");

							await engine.Expect
								.Text("2").In("label");
						});

					await RunTestAsync(
                        testCase.DriverType,
                        serviceProvider,
						async engine =>
						{
							await engine.OpenTest(server, "engine/select.html");

							await engine.Select.ByValue("2").From("select");

							await engine.Expect
								.Text("2").In("label");
						});

					await RunTestAsync(
                        testCase.DriverType,
                        serviceProvider,
						async engine =>
						{
							await engine.OpenTest(server, "engine/select.html");

							await engine.Select.ByValue(2).From("select");

							await engine.Expect
								.Text("2").In("label");
						});

					await RunTestAsync(
                        testCase.DriverType,
                        serviceProvider,
						async engine =>
						{
							await engine.OpenTest(server, "engine/select.html");

							await engine.Select.ByValue((object)2).From("select");

							await engine.Expect
								.Text("2").In("label");
						});

					await RunTestAsync(
                        testCase.DriverType,
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
                        testCase.DriverType,
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
                        testCase.DriverType,
                        serviceProvider,
						async engine =>
						{
							await engine.OpenTest(server, "engine/wait-until.html");

                            await engine.Click.On("button");

							await engine.Wait(until => until
								.Text("loaded").In("button"));
						});

					await RunTestAsync(
                        testCase.DriverType,
                        serviceProvider,
						async engine =>
						{
							await engine.OpenTest(server, "engine/click.html");

							var clickedButtons = await engine.Click.On("button");
							var clickedButton = clickedButtons.Single();
							Assert.AreEqual("clicked", clickedButton.TextContent);
						});

					await RunTestAsync(
                        testCase.DriverType,
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
                }
			}
		}

        [DebuggerHidden]
		private async Task RunTestAsync(
            DriverType driverType,
            IServiceProvider serviceProvider, 
            Func<IWebAutomationEngine, Task> run)
		{
            try
            {
                using (var automationEngine = serviceProvider.GetRequiredService<IWebAutomationEngine>())
                {
                    await automationEngine.InitializeAsync();
                    await run(automationEngine);
                }
            } catch(ExpectationNotMetException ex)
            {
                throw new Exception(driverType.ToString() + " failure.", ex);
            }
		}
	}
}
