using FluffySpoon.Automation.Web.Dom;

namespace FluffySpoon.Automation.Web.Fluent
{
    public interface IEnterMethodChainNode : IBaseMethodChainNode
    {
        IDefaultMethodChainNode In(string selector);
        IDefaultMethodChainNode In(IDomElement element);
    }
}