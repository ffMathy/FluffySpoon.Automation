namespace FluffySpoon.Automation.Web.Fluent
{
    class MethodChainQueueFactory: IMethodChainQueueFactory
    {
        public IMethodChainQueue Create()
        {
            return new MethodChainQueue();
        }
    }
}
