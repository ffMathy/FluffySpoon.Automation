using FluffySpoon.Automation.Web.Dom;

namespace FluffySpoon.Automation.Web.Fluent.Expect
{
    public interface IExpectTextMethodChainNode: IBaseMethodChainNode
    {
        IExpectMethodChainNode In(string selector);
        IExpectMethodChainNode In(IDomElement element);
    }
}