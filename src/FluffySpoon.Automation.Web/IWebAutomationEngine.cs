using System.Collections.Generic;
using System.Threading.Tasks;
using FluffySpoon.Automation.Web.Fluent;

namespace FluffySpoon.Automation.Web
{
    public interface IWebAutomationEngine: IDefaultMethodChainNode
    {
		Task InitializeAsync();
    }
}