using FluffySpoon.Automation.Web.Fluent.Targets;
using System.Threading.Tasks;

namespace FluffySpoon.Automation.Web.Fluent.Focus
{
	class FocusMethodChainNode : BaseDomElementTargetMethodChainNode<IBaseMethodChainNode, FocusMethodChainNode, FocusOnTargetMethodChainNode>
	{
		public override IBaseMethodChainNode Clone()
		{
			return new FocusMethodChainNode();
		}
	}
}
