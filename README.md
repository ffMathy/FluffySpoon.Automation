# FluffySpoon.Automation
An abstraction that makes Selenium or Puppeteer testing fun, stable and fast.

<b>This is still not ready for production-use and is experimental! There are still open issues on it, and it is not 100% bug-free.</b>

## Selectors
A selector determines how to select elements. Right now, only jQuery selectors are supported.

### jQuery
```
install-package FluffySpoon.Automation.JQuery
```

## Automation frameworks
An automation framework decides how the automation is done. Can use either Selenium or Puppeteer currently.

### Selenium
```
install-package FluffySpoon.Automation.Selenium
```

### Puppeteer
```
install-package FluffySpoon.Automation.Puppeteer
```

## Example
The following test searches for something in Google and asserts that the results are present in both Chrome, Edge and Firefox on Selenium, and Chromium on Puppeteer.

```csharp
var serviceCollection = new ServiceCollection();
serviceCollection.UseJQueryDomSelector();

//use 3 different browsers via Selenium
serviceCollection.AddSeleniumWebAutomationFrameworkInstance(GetEdgeDriver);
serviceCollection.AddSeleniumWebAutomationFrameworkInstance(GetFirefoxDriver);
serviceCollection.AddSeleniumWebAutomationFrameworkInstance(GetChromeDriver);

//also use Chromium via Puppeteer
serviceCollection.AddPuppeteerWebAutomationFrameworkInstance(GetPuppeteerDriverAsync);

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
