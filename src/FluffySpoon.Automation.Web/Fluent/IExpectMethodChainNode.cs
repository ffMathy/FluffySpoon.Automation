namespace FluffySpoon.Automation.Web.Fluent
{
    public interface IExpectMethodChainNode: IBaseMethodChainNode
    {
        IExpectTextMethodChainNode Text(string text);
        
        IExpectMethodChainNode Value(int value);
        IExpectMethodChainNode Value(string value);
    }
}