using FluffySpoon.Automation.Web.Fluent.Targets;

namespace FluffySpoon.Automation.Web.Fluent.Expect.Count
{
	class ExpectCountMethodChainNode : BaseDomElementTargetsMethodChainNode<IBaseMethodChainNode, ExpectCountMethodChainNode, ExpectCountOfTargetsMethodChainNode>
	{
		internal int Count { get; }

		public ExpectCountMethodChainNode(int count)
		{
			Count = count;
		}

		public override IBaseMethodChainNode Clone()
		{
			var clone = new ExpectCountMethodChainNode(Count);
			TransferDelegation(clone);

			return clone;
		}
	}
}
