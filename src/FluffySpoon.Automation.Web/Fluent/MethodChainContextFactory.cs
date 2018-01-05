using System.Collections;

namespace FluffySpoon.Automation.Web.Fluent
{
    class MethodChainContextFactory: IMethodChainContextFactory
    {
        public MethodChainContextFactory()
        {
            
        }

        public IMethodChainContext Create()
        {
            return new MethodChainContext();
        }
    }
}
