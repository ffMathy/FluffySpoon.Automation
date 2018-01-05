using FluffySpoon.Automation.Web.Dom;
using System;
using System.Collections.Generic;
using System.Text;

namespace FluffySpoon.Automation.Web.JQuery
{
	class JQueryDomSelectorStrategyFactory : IDomSelectorStrategyFactory
	{
		public IDomSelectorStrategy Create(IWebAutomationFrameworkInstance framework)
		{
			return new JQueryDomSelectorStrategy(framework);
		}
	}
}
