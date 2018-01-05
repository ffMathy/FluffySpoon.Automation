using System;
using System.Collections.Generic;
using System.Text;

namespace FluffySpoon.Automation.Web.Dom
{
    public interface IDomSelectorStrategyFactory
    {
		IDomSelectorStrategy Create(IWebAutomationFrameworkInstance framework);
    }
}
