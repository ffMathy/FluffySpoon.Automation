using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace FluffySpoon.Automation.Web.Dom
{
    public interface IDomSelectorStrategy
	{
		string GetJavaScriptForRetrievingDomElements(string selector);
	}
}
