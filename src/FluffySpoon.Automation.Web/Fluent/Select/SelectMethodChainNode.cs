using System.Linq;
using System.Threading.Tasks;
using FluffySpoon.Automation.Web.Fluent.Targets;
using FluffySpoon.Automation.Web.Fluent.Targets.Of;

namespace FluffySpoon.Automation.Web.Fluent.Select
{
	class SelectMethodChainNode: BaseDomElementTargetsMethodChainNode<IBaseMethodChainNode, SelectMethodChainNode, SelectByFromTargetMethodChainNode>,
		ISelectMethodChainNode
    {
		public ISelectByMethodChainNode ByIndex(int index)
		{
			return ByIndices(new[] { index });
		}

		public ISelectByMethodChainNode ByIndices(params int[] indices)
		{
			return MethodChainContext.Enqueue(SelectByMethodChainNode.ByIndices(indices));
		}

		public ISelectByMethodChainNode ByText(string text)
		{
			return ByTexts(new[] { text });
		}

		public ISelectByMethodChainNode ByTexts(params string[] texts)
		{
			return MethodChainContext.Enqueue(SelectByMethodChainNode.ByTexts(texts));
		}

		public ISelectByMethodChainNode ByValue(object value)
		{
			return ByValues(new[] { value });
		}

		public ISelectByMethodChainNode ByValue(string value)
		{
			return ByValues(new[] { value });
		}

		public ISelectByMethodChainNode ByValue(int value)
		{
			return ByValues(new[] { value });
		}

		public ISelectByMethodChainNode ByValues(params object[] values)
		{
			return ByValues(values
				.Select(x => x.ToString())
				.ToArray());
		}

		public ISelectByMethodChainNode ByValues(params string[] values)
		{
			return MethodChainContext.Enqueue(SelectByMethodChainNode.ByValues(values));
		}

		public ISelectByMethodChainNode ByValues(params int[] values)
		{
			return ByValues(values
				.Cast<object>()
				.ToArray());
		}
	}
}