using System.Collections.Generic;
using FluffySpoon.Automation.Web.Fluent.Root;
using System.Threading.Tasks;
using FluffySpoon.Automation.Web.Dom;
using System;

namespace FluffySpoon.Automation.Web.Fluent.Select
{
    class SelectByFromTargetMethodChainNode: MethodChainRoot<SelectByMethodChainNode>, ISelectByFromTargetMethodChainNode
	{
		protected override bool MayCauseElementSideEffects => true;

		public override IReadOnlyList<IDomElement> Elements
		{
			get => Parent.Elements;
			protected internal set => Parent.Elements = value;
		}

        protected override async Task OnExecuteAsync(IWebAutomationFrameworkInstance framework)
        {
			if (Parent.Indices == null && Parent.Texts == null && Parent.Values == null)
				throw new InvalidOperationException("Must select by indices, texts or values.");

			if(Parent.Indices != null)
				await framework.SelectByIndicesAsync(Elements, Parent.Indices);

			if (Parent.Texts != null)
				await framework.SelectByTextsAsync(Elements, Parent.Texts);

			if (Parent.Values != null)
				await framework.SelectByValuesAsync(Elements, Parent.Values);

			await base.OnExecuteAsync(framework);
        }

		public override IBaseMethodChainNode Clone()
		{
			return new SelectByFromTargetMethodChainNode();
		}
	}
}
