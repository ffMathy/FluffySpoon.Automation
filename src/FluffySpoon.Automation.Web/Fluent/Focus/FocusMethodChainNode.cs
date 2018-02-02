using FluffySpoon.Automation.Web.Fluent.Targets;
using System.Threading.Tasks;

namespace FluffySpoon.Automation.Web.Fluent.Focus
{
	class FocusMethodChainNode : BaseMouseTargetMethodChainNode<IBaseMethodChainNode, FocusMethodChainNode, FocusOnTargetMethodChainNode>
	{
		public override IBaseMethodChainNode Clone()
		{
			return new FocusMethodChainNode();
		}
	}
}
