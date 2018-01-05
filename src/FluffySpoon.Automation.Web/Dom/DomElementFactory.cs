using System;
using System.Collections.Generic;
using System.Text;

namespace FluffySpoon.Automation.Web.Dom
{
	class DomElementFactory : IDomElementFactory
	{
		public IDomElement Create(string cssSelector)
		{
			return new DomElement()
			{
				CssSelector = cssSelector
			};
		}
	}
}
