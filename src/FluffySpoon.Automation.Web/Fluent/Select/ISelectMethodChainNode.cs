using FluffySpoon.Automation.Web.Fluent.Root;
using FluffySpoon.Automation.Web.Fluent.Targets.From;
using FluffySpoon.Automation.Web.Fluent.Targets.Of;

namespace FluffySpoon.Automation.Web.Fluent.Select
{
	public interface ISelectMethodChainNode : 
		IBaseMethodChainNode
	{
		IDomElementFromTargetsMethodChainNode<IBaseMethodChainNode, ISelectByFromTargetMethodChainNode> ByIndex(int index);
		IDomElementFromTargetsMethodChainNode<IBaseMethodChainNode, ISelectByFromTargetMethodChainNode> ByIndices(params int[] indexes);

		IDomElementFromTargetsMethodChainNode<IBaseMethodChainNode, ISelectByFromTargetMethodChainNode> ByValue(object value);
		IDomElementFromTargetsMethodChainNode<IBaseMethodChainNode, ISelectByFromTargetMethodChainNode> ByValue(string value);
		IDomElementFromTargetsMethodChainNode<IBaseMethodChainNode, ISelectByFromTargetMethodChainNode> ByValue(int value);

		IDomElementFromTargetsMethodChainNode<IBaseMethodChainNode, ISelectByFromTargetMethodChainNode> ByValues(params object[] values);
		IDomElementFromTargetsMethodChainNode<IBaseMethodChainNode, ISelectByFromTargetMethodChainNode> ByValues(params string[] values);
		IDomElementFromTargetsMethodChainNode<IBaseMethodChainNode, ISelectByFromTargetMethodChainNode> ByValues(params int[] values);

		IDomElementFromTargetsMethodChainNode<IBaseMethodChainNode, ISelectByFromTargetMethodChainNode> ByText(string text);
		IDomElementFromTargetsMethodChainNode<IBaseMethodChainNode, ISelectByFromTargetMethodChainNode> ByTexts(params string[] texts);
	}
}