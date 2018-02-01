using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FluffySpoon.Automation.Web.Fluent.Context
{
    public interface IMethodChainContext: IAwaitable
    {
        Task RunAllAsync();
        Task RunNextAsync();

		IEnumerable<IWebAutomationFrameworkInstance> Frameworks { get; }
		TMethodChainNode Enqueue<TMethodChainNode>(Func<TMethodChainNode> node) where TMethodChainNode : class, IBaseMethodChainNode;
    }
}