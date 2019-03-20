using FluffySpoon.Automation.Web.Dom;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FluffySpoon.Automation.Web.Fluent.Targets
{
	abstract class BaseDomElementTargetMethodChainNode<TParentMethodChainNode, TCurrentMethodChainNode, TNextMethodChainNode> :
		BaseMethodChainNode<TParentMethodChainNode>,
		IBaseDomElementTargetMethodChainNode<TCurrentMethodChainNode, TNextMethodChainNode>
		where TNextMethodChainNode : IBaseMethodChainNode, new()
		where TCurrentMethodChainNode : IBaseMethodChainNode
		where TParentMethodChainNode : IBaseMethodChainNode
	{
		private string _selector;

		private BaseDomElementTargetMethodChainNode<TParentMethodChainNode, TCurrentMethodChainNode, TNextMethodChainNode> _delegatedFrom;

		protected override bool MayCauseElementSideEffects => false;

		protected TNextMethodChainNode Delegate(string selector)
		{
			_selector = selector;
			return Delegate();
		}

		protected TNextMethodChainNode Delegate(IDomElement element)
		{
			return Delegate(new[] { element });
		}

		protected TNextMethodChainNode Delegate(IReadOnlyList<IDomElement> elements)
		{
			Elements = elements;
			return Delegate();
		}

		private TNextMethodChainNode Delegate()
		{
			lock (MethodChainContext)
			{
				return MethodChainContext.Enqueue(new TNextMethodChainNode());
			}
		}

		protected void TransferDelegation(BaseDomElementTargetMethodChainNode<TParentMethodChainNode, TCurrentMethodChainNode, TNextMethodChainNode> target)
		{
			target._delegatedFrom = this;
		}

		protected override async Task OnExecuteAsync(IWebAutomationFrameworkInstance framework)
		{
            if (_delegatedFrom != null)
            {
                if(_selector == null)
                    _selector = _delegatedFrom._selector;

                if(Elements == null && _delegatedFrom.Elements != null && _delegatedFrom.Elements.Count > 0)
                    Elements = _delegatedFrom.Elements;
            }

			var hasNoElements = Elements == null;
			if (hasNoElements && _selector == null)
				throw new InvalidOperationException("Elements to target must be found either via a selector or a list of elements.");

			if (hasNoElements) { 
				Elements = await framework.FindDomElementsBySelectorAsync(
					MethodChainOffset,
					_selector);
			}

			if (_delegatedFrom != null)
				_delegatedFrom.Elements = Elements;

			await base.OnExecuteAsync(framework);

			if (_delegatedFrom != null)
				_delegatedFrom.Elements = Elements;
		}

		public TNextMethodChainNode In(string selector) => Delegate(selector);
		public TNextMethodChainNode In(IDomElement element) => Delegate(element);

		public TNextMethodChainNode Of(string selector) => Delegate(selector);
		public TNextMethodChainNode Of(IDomElement element) => Delegate(element);

		public TNextMethodChainNode From(string selector) => Delegate(selector);
		public TNextMethodChainNode From(IDomElement element) => Delegate(element);

		public TNextMethodChainNode On(string selector) => Delegate(selector);
		public TNextMethodChainNode On(IDomElement element) => Delegate(element);

		public TNextMethodChainNode At(string selector) => Delegate(selector);
		public TNextMethodChainNode At(IDomElement element) => Delegate(element);

		public TNextMethodChainNode To(string selector) => Delegate(selector);
		public TNextMethodChainNode To(IDomElement element) => Delegate(element);
	}
}
