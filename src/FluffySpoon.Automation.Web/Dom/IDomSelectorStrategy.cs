using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace FluffySpoon.Automation.Web.Dom
{
    public interface IDomSelectorStrategy
	{
		Task<IReadOnlyList<IDomElement>> GetDomElementsAsync(string selector);
	}
}
