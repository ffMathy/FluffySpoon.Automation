using Microsoft.Extensions.DependencyInjection;
using FluffySpoon.Automation.Web.JQuery;
using FluffySpoon.Automation.Web.Puppeteer;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Linq;

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

                    try
                    {
                        await RunTestAsync(
                            serviceProvider,
                            async engine =>
                            {
                                await engine.OpenTest("engine/select.html");

                                await engine.Select.ByText("Bar").From("select");

                                var selectElements = await engine.Find("label");
                                var selectElement = selectElements.Single();
                                Assert.AreEqual("vbar", selectElement.TextContent);
                            });

                        await RunTestAsync(
                            serviceProvider,
                            async engine =>
                            {
                                await engine.OpenTest("engine/select.html");

                                await engine.Select.ByIndex(1).From("select");

                                var selectElements = await engine.Find("label");
                                var selectElement = selectElements.Single();
                                Assert.AreEqual("vbar", selectElement.TextContent);
                            });

                        await RunTestAsync(
                            serviceProvider,
                            async engine =>
                            {
                                await engine.OpenTest("engine/select.html");

                                await engine.Select.ByValue("vbar").From("select");

                                var selectElements = await engine.Find("label");
                                var selectElement = selectElements.Single();
                                Assert.AreEqual("vbar", selectElement.TextContent);
                            });

                        await RunTestAsync(
                            serviceProvider,
                            async engine =>
                            {
                                await engine.OpenTest("engine/double-click.html");

                                var clickedButtons = await engine.DoubleClick.On("button");
                                var clickedButton = clickedButtons.Single();
                                Assert.AreEqual("clicked", clickedButton.TextContent);
                            });

                        await RunTestAsync(
                            serviceProvider,
                            async engine =>
                            {
                                await engine.OpenTest("engine/right-click.html");

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
                                await engine.OpenTest("engine/wait-until.html");

                                var clickedButtons = await engine.Click.On("button");
                                var clickedButton = clickedButtons.Single();
                                Assert.AreEqual("loading", clickedButton.TextContent);

                                var newClickedButtons = await engine
                                    .Wait(until => until
                                        .Text("loaded").In("button"))
                                    .Find("button");
                                var newClickedButton = newClickedButtons.Single();
                                Assert.AreEqual("loaded", newClickedButton.TextContent);

                            });

                        await RunTestAsync(
                            serviceProvider,
                            async engine =>
                            {
                                await engine.OpenTest("engine/click.html");

                                var clickedButtons = await engine.Click.On("button");
                                var clickedButton = clickedButtons.Single();
                                Assert.AreEqual("clicked", clickedButton.TextContent);
                            });

                        await RunTestAsync(
                            serviceProvider,
                            async x =>
                            {
                                await x.OpenTest("engine/find.html");

                                var aMatches = await x.Find(".a");
                                var bMatches = await x.Find(".b");
                                var cMatches = await x.Find(".c");

                                Assert.AreEqual(6, aMatches.Count);
                                Assert.AreEqual(3, bMatches.Count);
                                Assert.AreEqual(2, cMatches.Count);
                            });
                    }
                    catch (AssertFailedException)
                    {
                        throw;
                    }
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
