using FluffySpoon.Automation.Web.Dom;

namespace FluffySpoon.Automation.Web.Fluent.Enter
{
    class EnterMethodChainNode: BaseMethodChainNode, IEnterMethodChainNode
    {
        internal string TextToEnter { get; }

        public EnterMethodChainNode(string text)
        {
            TextToEnter = text;
        }

        public IDefaultMethodChainNode In(string selector)
        {
            return MethodChainContext.Enqueue(new EnterInMethodChainNode(
                this,
                selector));
        }

        public IDefaultMethodChainNode In(IDomElement element)
        {
            return In(element.CssSelector);
        }
    }
}