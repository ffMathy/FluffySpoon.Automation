using System.Threading.Tasks;
using FluffySpoon.Automation.Web.Fluent.Targets;

namespace FluffySpoon.Automation.Web.Fluent.Select
{
    internal class SelectByMethodChainNode: BaseDomElementTargetsMethodChainNode<IBaseMethodChainNode, SelectByMethodChainNode, SelectByFromTargetMethodChainNode>
	{
		internal string[] Values { get; private set; }
		internal int[] Indices { get; private set; }
		internal string[] Texts { get; private set; }

		private SelectByMethodChainNode() { }
		
		internal static SelectByMethodChainNode ByValues(string[] values)
		{
			return new SelectByMethodChainNode()
			{
				Values = values
			};
		}

        internal static SelectByMethodChainNode ByTexts(string[] texts)
		{
			return new SelectByMethodChainNode()
			{
				Texts = texts
			};
		}

        internal static SelectByMethodChainNode ByIndices(int[] indices)
		{
			return new SelectByMethodChainNode()
			{
				Indices = indices
			};
		}

		protected override bool MayCauseElementSideEffects => true;

        protected override Task OnExecuteAsync(IWebAutomationFrameworkInstance framework)
        {
            return base.OnExecuteAsync(framework);
        }

        public override IBaseMethodChainNode Clone()
		{
			var node = new SelectByMethodChainNode() {
				Indices = Indices,
				Texts = Texts,
				Values = Values
			};
            TransferDelegation(node);

            return node;
        }
	}
}
