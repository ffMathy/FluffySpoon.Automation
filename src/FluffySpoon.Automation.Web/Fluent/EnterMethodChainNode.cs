namespace FluffySpoon.Automation.Web.Fluent
{
    class EnterMethodChainNode: BaseMethodChainNode, IEnterMethodChainNode
    {
        public string TextToEnter { get; }

        public EnterMethodChainNode(
            IMethodChainContext methodChainContext,
            string text) : base(methodChainContext)
        {
            TextToEnter = text;
        }

        public IDefaultMethodChainNode In(string selector)
        {
            return MethodChainContext.Enqueue(new EnterInMethodChainNode(
                this,
                MethodChainContext,
                selector));
        }

        public IDefaultMethodChainNode In(IDomElement element)
        {
            return In(element.Selector);
        }
    }
}