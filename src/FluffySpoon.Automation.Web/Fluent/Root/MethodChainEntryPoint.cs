namespace FluffySpoon.Automation.Web.Fluent.Root
{
	class MethodChainEntryPoint: MethodChainRoot<IBaseMethodChainNode>
	{
		protected override bool MayCauseElementSideEffects => false;

		public override IBaseMethodChainNode Clone()
		{
			return this;
		}
	}
}
