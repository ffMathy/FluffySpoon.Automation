using FluffySpoon.Automation.Web.Fluent.Targets;
using System.Threading.Tasks;

namespace FluffySpoon.Automation.Web.Fluent.Click
{
	class ClickMethodChainNode : BaseMouseTargetsMethodChainNode<IBaseMethodChainNode, ClickMethodChainNode, ClickOnTargetsMethodChainNode>
	{
		public override IBaseMethodChainNode Clone()
		{
			return new ClickMethodChainNode();
		}
	}
}
