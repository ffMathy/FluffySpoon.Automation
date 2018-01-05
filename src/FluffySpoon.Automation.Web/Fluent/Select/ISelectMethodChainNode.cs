using FluffySpoon.Automation.Web.Dom;

namespace FluffySpoon.Automation.Web.Fluent
{
    public interface ISelectMethodChainNode: IBaseMethodChainNode
    {
        IDefaultMethodChainNode From(string selector);
        IDefaultMethodChainNode From(IDomElement element);
    }
}