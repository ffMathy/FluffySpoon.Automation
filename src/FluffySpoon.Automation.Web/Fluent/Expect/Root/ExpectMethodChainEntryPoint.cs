namespace FluffySpoon.Automation.Web.Fluent.Expect.Root
{
	class ExpectMethodChainEntryPoint : ExpectMethodChainRoot<IBaseMethodChainNode>
	{

		protected override bool MayCauseElementSideEffects => false;

		public ExpectMethodChainEntryPoint()
		{

		}

		public override IBaseMethodChainNode Clone()
		{
			return new ExpectMethodChainEntryPoint();
		}
	}
}
