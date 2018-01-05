using System.Collections.Generic;
using FluffySpoon.Automation.Web.Fluent;

namespace FluffySpoon.Automation.Web
{
    public interface IWebAutomationEngine: IDefaultMethodChainNode
    {
        void Configure(IEnumerable<IWebAutomationTechnology> technologies);
        void Configure(IWebAutomationTechnology technology);
    }
}