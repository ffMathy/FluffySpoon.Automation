using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FluffySpoon.Automation.Web.Fluent.Context
{
	public interface IMethodChainContext : IAwaitable
	{
		Task RunAllAsync();
		Task RunNextAsync();

		int NodeCount { get; }

		IEnumerable<IWebAutomationFrameworkInstance> Frameworks { get; }
		IWebAutomationEngine AutomationEngine { get; }
		TMethodChainNode Enqueue<TMethodChainNode>(TMethodChainNode node) where TMethodChainNode : IBaseMethodChainNode;
	}
}