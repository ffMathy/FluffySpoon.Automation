using System;

namespace FluffySpoon.Automation.Web.Fluent
{
    class DefaultMethodChainNode: BaseMethodChainNode, IDefaultMethodChainNode
    {

        public DefaultMethodChainNode(
            IMethodChainQueue methodChainQueue) : base(methodChainQueue)
        {
        }

        public IOpenMethodChainNode Open(string uri)
        {
            return MethodChainQueue.Enqueue(new OpenMethodChainNode(
                MethodChainQueue,
                uri));
        }

        public IOpenMethodChainNode Open(Uri uri)
        {
            return Open(uri.ToString());
        }

        public ISelectMethodChainNode Select(string value)
        {
            throw new NotImplementedException();
        }

        public ISelectMethodChainNode Select(int index)
        {
            throw new NotImplementedException();
        }

        public IEnterMethodChainNode Enter(string text)
        {
            return MethodChainQueue.Enqueue(new EnterMethodChainNode(
                MethodChainQueue,
                text));
        }

        public IExpectMethodChainNode Expect { get; }
    }
}
