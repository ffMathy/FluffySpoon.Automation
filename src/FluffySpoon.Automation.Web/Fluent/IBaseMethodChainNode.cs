using FluffySpoon.Automation.Web.Fluent.Context;
using System.Threading.Tasks;

namespace FluffySpoon.Automation.Web.Fluent
{
	public interface IBaseMethodChainNode : IBaseMethodChainNode<IBaseMethodChainNode>
	{
		IMethodChainContext MethodChainContext { set; }

		Task ExecuteAsync(IWebAutomationFrameworkInstance framework);
	}

	public interface IBaseMethodChainNode<in TParentMethodChainNode> where TParentMethodChainNode : IBaseMethodChainNode
	{
		TParentMethodChainNode Parent { set; }
	}
}