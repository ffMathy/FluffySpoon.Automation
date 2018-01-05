using System;

namespace FluffySpoon.Automation.Web.Fluent
{
    public interface IDefaultMethodChainNode: IBaseMethodChainNode
    {
        IOpenMethodChainNode Open(string uri);
        IOpenMethodChainNode Open(Uri uri);

        ISelectMethodChainNode Select(string value);
        ISelectMethodChainNode Select(int index);

        IEnterMethodChainNode Enter(string text);

        IExpectMethodChainNode Expect { get; }
    }
}