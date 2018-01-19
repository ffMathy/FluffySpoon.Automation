using System.Collections.Generic;
using FluffySpoon.Automation.Web.Fluent.Context;
using System.Threading.Tasks;
using FluffySpoon.Automation.Web.Dom;

namespace FluffySpoon.Automation.Web.Fluent
{
	public interface IBaseMethodChainNode
	{
		IMethodChainContext MethodChainContext { set; }
		IReadOnlyList<IDomElement> Elements { get; }

		Task ExecuteAsync(IWebAutomationFrameworkInstance framework);
	}

	public interface IBaseMethodChainNode<in TParentMethodChainNode> : IBaseMethodChainNode where TParentMethodChainNode : IBaseMethodChainNode
	{
		TParentMethodChainNode Parent { set; }
	}
}