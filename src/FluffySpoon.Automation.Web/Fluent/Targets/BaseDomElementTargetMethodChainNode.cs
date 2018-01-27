using FluffySpoon.Automation.Web.Dom;
using System;
using System.Collections.Generic;
using FluffySpoon.Automation.Web.Fluent.Find;
using System.Threading.Tasks;

namespace FluffySpoon.Automation.Web.Fluent.Targets
{
	abstract class BaseDomElementTargetMethodChainNode<TCurrentMethodChainNode, TNextMethodChainNode> :
		BaseMethodChainNode<IBaseMethodChainNode>,
		IBaseDomElementTargetMethodChainNode<TCurrentMethodChainNode, TNextMethodChainNode> 
		where TNextMethodChainNode : class, IBaseMethodChainNode, new()
		where TCurrentMethodChainNode : IBaseMethodChainNode
	{
		private string _selector;

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
				MethodChainContext.Enqueue(this);
				return MethodChainContext.Enqueue(new TNextMethodChainNode());
			}
		}

		protected override async Task OnExecuteAsync(IWebAutomationFrameworkInstance framework)
		{
			if(Elements == null) {
				if (_selector == null)
					throw new InvalidOperationException("Elements to target must be found either via a selector or a list of elements.");

				var findNode = new FindMethodChainNode(_selector);
				await findNode.ExecuteAsync(framework);
				
				Elements = findNode.Elements ?? 
					throw new InvalidOperationException("The web driver returned null when trying to get elements by selector \"" + _selector + "\".");
			}

			await base.OnExecuteAsync(framework);
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
	}
}
