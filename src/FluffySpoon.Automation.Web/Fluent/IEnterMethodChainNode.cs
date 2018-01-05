namespace FluffySpoon.Automation.Web.Fluent
{
    public interface IEnterMethodChainNode : IBaseMethodChainNode
    {
        string TextToEnter { get; }

        IDefaultMethodChainNode In(string selector);
        IDefaultMethodChainNode In(IDomElement element);
    }
}