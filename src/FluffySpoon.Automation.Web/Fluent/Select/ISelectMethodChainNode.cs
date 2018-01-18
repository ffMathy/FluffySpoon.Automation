namespace FluffySpoon.Automation.Web.Fluent.Select
{
	public interface ISelectMethodChainNode: IBaseMethodChainNode
	{
		ISelectByMethodChainNode ByValue(string value);
		ISelectByMethodChainNode ByText(string text);
		ISelectByMethodChainNode ByIndex(int index);
    }
}