namespace FluffySpoon.Automation.Web.Fluent
{
    class EnterMethodChainNode: BaseMethodChainNode, IEnterMethodChainNode
    {
        public string TextToEnter { get; }

        public EnterMethodChainNode(
            IMethodChainQueue methodChainQueue,
            string text) : base(methodChainQueue)
        {
            TextToEnter = text;
        }

        public IDefaultMethodChainNode In(string selector)
        {
            return MethodChainQueue.Enqueue(new EnterInMethodChainNode(
                this,
                MethodChainQueue,
                selector));
        }

        public IDefaultMethodChainNode In(IDomElement element)
        {
            return In(element.Selector);
        }
    }
}