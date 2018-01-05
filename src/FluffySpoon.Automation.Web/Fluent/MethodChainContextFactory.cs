using System.Collections;
using System.Collections.Generic;

namespace FluffySpoon.Automation.Web.Fluent
{
    class MethodChainContextFactory: IMethodChainContextFactory
    {
		private readonly IEnumerable<IWebAutomationFrameworkInstance> _frameworks;

		public MethodChainContextFactory(IEnumerable<IWebAutomationFrameworkInstance> frameworks)
        {
			_frameworks = frameworks;
		}

        public IMethodChainContext Create()
        {
            return new MethodChainContext(_frameworks);
        }
    }
}
