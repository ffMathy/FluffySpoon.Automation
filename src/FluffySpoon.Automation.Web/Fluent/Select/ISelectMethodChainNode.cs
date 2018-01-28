using FluffySpoon.Automation.Web.Fluent.Root;
using FluffySpoon.Automation.Web.Fluent.Targets.Of;

namespace FluffySpoon.Automation.Web.Fluent.Select
{
	public interface ISelectMethodChainNode : 
		IBaseMethodChainNode
	{
		ISelectByMethodChainNode ByIndex(int index);
		ISelectByMethodChainNode ByIndices(params int[] indexes);

		ISelectByMethodChainNode ByValue(object value);
		ISelectByMethodChainNode ByValue(string value);
		ISelectByMethodChainNode ByValue(int value);

		ISelectByMethodChainNode ByValues(params object[] values);
		ISelectByMethodChainNode ByValues(params string[] values);
		ISelectByMethodChainNode ByValues(params int[] values);

		ISelectByMethodChainNode ByText(string text);
		ISelectByMethodChainNode ByTexts(params string[] texts);
	}
}