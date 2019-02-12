using System;
using System.Threading;
using System.Threading.Tasks;
using FluffySpoon.Automation.Web.Fluent.Root;

namespace FluffySpoon.Automation.Web
{
	public interface IWebAutomationEngine : IMethodChainRoot, IDisposable
	{
		Task InitializeAsync();
		SynchronizationContext SynchronizationContext { get; }
	}
}