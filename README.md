An abstraction that makes Selenium or Puppeteer testing fun, stable and fast.

# Selectors
A selector determines how to select elements. Right now, only jQuery selectors are supported.

## Css
```
install-package FluffySpoon.Automation.Css
```

```csharp
serviceCollection.AddJQueryDomSelector();
```

## jQuery
```
install-package FluffySpoon.Automation.JQuery
```

```csharp
serviceCollection.AddCssDomSelector();
```

# Automation frameworks
An automation framework decides how the automation is done. Can use either Selenium or Puppeteer currently.

## Selenium
```
install-package FluffySpoon.Automation.Selenium
```

## Puppeteer
```
install-package FluffySpoon.Automation.Puppeteer
```

# Example
The following test searches for something in Google and asserts that the results are present in both Chrome, Edge and Firefox on Selenium, and Chromium on Puppeteer.

```csharp
var serviceCollection = new ServiceCollection();
serviceCollection.AddJQueryDomSelector();

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

# Features

## Opening a URL
```csharp
await automationEngine.Open("https://example.com");
```

## Taking a screenshot
```csharp
await automationEngine
	.TakeScreenshot
	.Of("selector")
	.SaveAs((engine, i) => 
		engine.UserAgentName + "_" + i + ".jpg");
```

## Mouse operations

### Single clicking
```csharp
var elementsClicked = await automationEngine.Click.On("selector");
```

### Double clicking
```csharp
var elementsClicked = await automationEngine.DoubleClick.On("selector");
```

### Right clicking
```csharp
var elementsClicked = await automationEngine.RightClick.On("selector");
```

### Hovering an element
```csharp
var elementsHovered = await automationEngine.Hover.On("selector");
```

## Dragging & dropping
```csharp
var elementsDragged = await automationEngine.Drag.From("selector").To("selector");
```

## Typing in elements
```csharp
var elementsTypedIn = await automationEngine.Enter("some text").In("selector");
```

## Finding elements
```
var elements = await automationEngine.Find("selector");
```

## Focusing an element
*Currently only supports a single element.*

```csharp
var elementsFocused = await automationEngine.Focus.On("selector");
```

## Selecting from a `select` combo-box

### Multiple selections

#### Selecting by texts
```csharp
await automationEngine.Select.ByTexts("Bar", "Baz", ...).From("selector");
```

#### Selecting by indices
```csharp
await automationEngine.Select.ByIndices(0, 2, ...).From("selector");
```

#### Selecting by values
```csharp
await automationEngine.Select.ByValues("value1", "value2", ...).From("selector");
```

```
await automationEngine.Select.ByValues(1337, 1338, ...).From("selector");
```

### Single selection

#### Selecting by text
```csharp
await automationEngine.Select.ByText("Bar").From("selector");
```

#### Selecting by index
```csharp
await automationEngine.Select.ByIndex(0).From("selector");
```

#### Selecting by value
```csharp
await automationEngine.Select.ByValue("value").From("selector");
```

```
await automationEngine.Select.ByValue(1337).From("selector");
```

## Waiting
```csharp
await automationEngine.Wait(until => until.???);
```

_All methods that are available on the `until` object are the same as are available on the `Expect` object (see "Expectations" below).

## Expectations
Any expecation made that is not met, will throw an `ExpectationNotMetException`. If you want to wait until a specific expectation is met, see "Waiting" above instead.

### Specific class on an element
```csharp
await automationEngine.Expect.Class.Of("selector");
```

### Amount of elements that exist
```csharp
await automationEngine.Expect.Count(10).Of("selector");
```

### At least one element exists
```csharp
await automationEngine.Expect.Exists.Of("selector");
```

### Text in element
```csharp
await automationEngine.Expect.Text("my text").In("selector");
```

### Value of element
```csharp
await automationEngine.Expect.Value("value").Of("selector");
```

## More?
Open an issue if there's something missing. I want to make this library the best there is! Pull requests are also very welcome.

# Performance issues
There is a [known bug in .NET Core](https://github.com/SeleniumHQ/selenium/issues/6597) that will slow down Selenium, which has a workaround described.

If you see performance issues outside .NET Core or what you are seeing is unrelated, please open an issue.
