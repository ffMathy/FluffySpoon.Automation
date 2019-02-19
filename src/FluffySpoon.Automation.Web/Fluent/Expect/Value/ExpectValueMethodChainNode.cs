using FluffySpoon.Automation.Web.Fluent.Targets;

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
			var clone = new ExpectValueMethodChainNode(Value);
			TransferDelegation(clone);

			return clone;
		}
	}
}
