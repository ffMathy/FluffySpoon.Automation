using System;

namespace FluffySpoon.Automation.Web.Fluent
{
    class DefaultMethodChainNode: BaseMethodChainNode, IDefaultMethodChainNode
    {

        public DefaultMethodChainNode(
            IMethodChainContext methodChainContext) : base(methodChainContext)
        {
        }

        public IOpenMethodChainNode Open(string uri)
        {
            return MethodChainContext.Enqueue(new OpenMethodChainNode(
                MethodChainContext,
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
            return MethodChainContext.Enqueue(new EnterMethodChainNode(
                MethodChainContext,
                text));
        }

        public IExpectMethodChainNode Expect { get; }
    }
}
