using System;
using System.Collections.Generic;
using System.Text;

namespace FluffySpoon.Automation.Web.Fluent.Expect.Count
{
    public interface IExpectCountMethodChainNode : IBaseMethodChainNode
	{
		IExpectCountOfMethodChainNode Of(string selector);
    }
}
