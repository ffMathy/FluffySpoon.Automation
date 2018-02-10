using FluffySpoon.Automation.Web.Fluent.Targets;
using System.Threading.Tasks;

namespace FluffySpoon.Automation.Web.Fluent.Drag
{
	class DragMethodChainNode : BaseMouseTargetMethodChainNode<IBaseMethodChainNode, DragMethodChainNode, DragFromTargetMethodChainNode>
	{
		public override IBaseMethodChainNode Clone()
		{
			return new DragMethodChainNode();
		}
	}
}
