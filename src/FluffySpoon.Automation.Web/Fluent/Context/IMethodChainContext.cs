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
		TMethodChainNode Enqueue<TMethodChainNode>(TMethodChainNode node) where TMethodChainNode : IBaseMethodChainNode;
    }
}