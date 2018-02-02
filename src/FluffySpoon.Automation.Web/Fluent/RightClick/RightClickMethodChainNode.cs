using FluffySpoon.Automation.Web.Fluent.Targets;
using System.Threading.Tasks;

namespace FluffySpoon.Automation.Web.Fluent.RightClick
{
	class RightClickMethodChainNode : BaseMouseTargetsMethodChainNode<IBaseMethodChainNode, RightClickMethodChainNode, RightClickOnTargetsMethodChainNode>
	{
		public override IBaseMethodChainNode Clone()
		{
			return new RightClickMethodChainNode();
		}
	}
}
