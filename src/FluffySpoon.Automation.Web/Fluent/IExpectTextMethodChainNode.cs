namespace FluffySpoon.Automation.Web.Fluent
{
    public interface IExpectTextMethodChainNode: IBaseMethodChainNode
    {
        IExpectMethodChainNode In(string selector);
        IExpectMethodChainNode In(IDomElement element);
    }
}