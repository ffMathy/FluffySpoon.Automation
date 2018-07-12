# FluffySpoon.Automation
A set of automation tools.

## Example
The following test searches for something in Google and asserts that the results are present in both Chrome and Firefox.

```csharp
var serviceCollection = new ServiceCollection();
serviceCollection.UseJQueryDomSelector();
serviceCollection.AddSeleniumWebAutomationFrameworkInstance(GetFirefoxDriver);
serviceCollection.AddSeleniumWebAutomationFrameworkInstance(GetChromeDriver);

var serviceProvider = serviceCollection.BuildServiceProvider();

using (var automationEngine = serviceProvider.GetRequiredService<IWebAutomationEngine>())
{
	await automationEngine.InitializeAsync();

	await automationEngine
		.Open("https://google.com");
					
	await automationEngine
		.Enter("this is a very long test that works").In("input[type=text]:visible")
		.Wait(until => 
			until.Exists("input[type=submit]:visible"));

	var elements = await automationEngine
		.Click.On("input[type=submit]:visible:first")
		.Wait(until => 
			until.Exists("#rso .g:visible"))
		.Expect
		.Count(10).Of("#rso .g:visible");

	Console.WriteLine("Test done!");
}
```
