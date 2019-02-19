using FluffySpoon.Automation.Web.Fluent.Targets;

namespace FluffySpoon.Automation.Web.Fluent.Expect.Text
{
	class ExpectTextMethodChainNode : BaseDomElementTargetsMethodChainNode<IBaseMethodChainNode, ExpectTextMethodChainNode, ExpectTextInTargetsMethodChainNode>
	{
		internal string Text { get; }

		public ExpectTextMethodChainNode(string text)
		{
			Text = text;
		}

		public override IBaseMethodChainNode Clone()
		{
			var clone = new ExpectTextMethodChainNode(Text);
			TransferDelegation(clone);

			return clone;
		}
	}
}
