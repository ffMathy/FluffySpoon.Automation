using System.Linq;
using FluffySpoon.Automation.Web.Fluent.Targets;
using FluffySpoon.Automation.Web.Fluent.Targets.From;

namespace FluffySpoon.Automation.Web.Fluent.Select
{
	class SelectMethodChainNode: BaseDomElementTargetsMethodChainNode<IBaseMethodChainNode, SelectMethodChainNode, SelectByFromTargetMethodChainNode>,
		ISelectMethodChainNode
    {
		public IDomElementFromTargetsMethodChainNode<IBaseMethodChainNode, ISelectByFromTargetMethodChainNode> ByIndex(int index)
		{
			return ByIndices(new[] { index });
		}

		public IDomElementFromTargetsMethodChainNode<IBaseMethodChainNode, ISelectByFromTargetMethodChainNode> ByIndices(params int[] indices)
		{
			return MethodChainContext.Enqueue(SelectByMethodChainNode.ByIndices(indices));
		}

		public IDomElementFromTargetsMethodChainNode<IBaseMethodChainNode, ISelectByFromTargetMethodChainNode> ByText(string text)
		{
			return ByTexts(new[] { text });
		}

		public IDomElementFromTargetsMethodChainNode<IBaseMethodChainNode, ISelectByFromTargetMethodChainNode> ByTexts(params string[] texts)
		{
			return MethodChainContext.Enqueue(SelectByMethodChainNode.ByTexts(texts));
		}

		public IDomElementFromTargetsMethodChainNode<IBaseMethodChainNode, ISelectByFromTargetMethodChainNode> ByValue(object value)
		{
			return ByValues(new[] { value });
		}

		public IDomElementFromTargetsMethodChainNode<IBaseMethodChainNode, ISelectByFromTargetMethodChainNode> ByValue(string value)
		{
			return ByValues(new[] { value });
		}

		public IDomElementFromTargetsMethodChainNode<IBaseMethodChainNode, ISelectByFromTargetMethodChainNode> ByValue(int value)
		{
			return ByValues(new[] { value });
		}

		public IDomElementFromTargetsMethodChainNode<IBaseMethodChainNode, ISelectByFromTargetMethodChainNode> ByValues(params object[] values)
		{
			return ByValues(values
				.Select(x => x.ToString())
				.ToArray());
		}

		public IDomElementFromTargetsMethodChainNode<IBaseMethodChainNode, ISelectByFromTargetMethodChainNode> ByValues(params string[] values)
		{
			return MethodChainContext.Enqueue(SelectByMethodChainNode.ByValues(values));
		}

		public IDomElementFromTargetsMethodChainNode<IBaseMethodChainNode, ISelectByFromTargetMethodChainNode> ByValues(params int[] values)
		{
			return ByValues(values
				.Cast<object>()
				.ToArray());
		}

		public override IBaseMethodChainNode Clone()
		{
			return new SelectMethodChainNode();
		}
	}
}