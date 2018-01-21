using FluffySpoon.Automation.Web.Dom;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FluffySpoon.Automation.Web
{
	public interface IWebAutomationFrameworkInstance: IDisposable
	{
		Task<IReadOnlyList<IDomElement>> FindDomElementsAsync(string selector);
		Task<IReadOnlyList<IDomElement>> EvaluateJavaScriptAsDomElementsAsync(string code);
        Task<string> EvaluateJavaScriptAsync(string code);

        Task OpenAsync(string uri);
        Task EnterTextInAsync(IReadOnlyList<IDomElement> elements, string text);
		Task ClickAsync(IReadOnlyList<IDomElement> elements, int relativeX, int relativeY);
    }
}