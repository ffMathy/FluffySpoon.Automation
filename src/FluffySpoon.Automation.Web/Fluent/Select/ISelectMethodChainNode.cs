using FluffySpoon.Automation.Web.Dom;

namespace FluffySpoon.Automation.Web.Fluent.Select
{
    public interface ISelectMethodChainNode: IBaseMethodChainNode
    {
        IDefaultMethodChainNode From(string selector);
        IDefaultMethodChainNode From(IDomElement element);
    }
}