using System.Threading.Tasks;

namespace FluffySpoon.Automation.Web.Dom
{
	public interface IDomSelectorStrategy
	{
		Task InitializeAsync();

		string DomSelectorLibraryJavaScript { get; }
		
		string GetJavaScriptForRetrievingDomElements(string selector);
	}
}
