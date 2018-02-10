using FluffySpoon.Automation.Web.Fluent.Targets;
using System.Threading.Tasks;

namespace FluffySpoon.Automation.Web.Fluent.Hover
{
	class HoverMethodChainNode : BaseMouseTargetMethodChainNode<IBaseMethodChainNode, HoverMethodChainNode, HoverOnTargetMethodChainNode>
	{
		public override IBaseMethodChainNode Clone()
		{
			return new HoverMethodChainNode();
		}
	}
}
