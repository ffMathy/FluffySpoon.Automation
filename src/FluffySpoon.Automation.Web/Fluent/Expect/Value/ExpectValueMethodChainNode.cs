using FluffySpoon.Automation.Web.Fluent.Targets;
using System.Threading.Tasks;

namespace FluffySpoon.Automation.Web.Fluent.Expect.Value
{
	class ExpectValueMethodChainNode : BaseDomElementTargetsMethodChainNode<IBaseMethodChainNode, ExpectValueMethodChainNode, ExpectValueInTargetsMethodChainNode>
	{
		internal string Value { get; }

		public ExpectValueMethodChainNode(string value)
		{
			Value = value;
		}

		public override IBaseMethodChainNode Clone()
		{
			return new ExpectValueMethodChainNode(Value);
		}
	}
}
