using System.Collections.Generic;
using System.Threading.Tasks;

namespace FluffySpoon.Automation.Web
{
	public interface IWebAutomationTechnology
	{
		Task<IDomElement> EvaluateJavaScriptAsDomElementAsync(string code);
		Task<IReadOnlyList<IDomElement>> EvaluateJavaScriptAsDomElementsAsync(string code);
        Task EvaluateJavaScriptAsync(string code);

        Task OpenAsync(string uri);
        Task EnterTextIn(string text, string selector);
    }
}