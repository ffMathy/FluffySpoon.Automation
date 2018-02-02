using FluffySpoon.Automation.Web.Fluent.Targets;
using System.Threading.Tasks;

namespace FluffySpoon.Automation.Web.Fluent.DoubleClick
{
	class DoubleClickMethodChainNode : BaseMouseTargetsMethodChainNode<IBaseMethodChainNode, DoubleClickMethodChainNode, DoubleClickOnTargetsMethodChainNode>
	{
		public override IBaseMethodChainNode Clone()
		{
			return new DoubleClickMethodChainNode();
		}
	}
}
