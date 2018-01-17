using System;
using FluffySpoon.Automation.Web.Dom;

namespace FluffySpoon.Automation.Web.Fluent.Expect
{
    public interface IExpectMethodChainNode: IBaseMethodChainNode
    {
        IExpectTextMethodChainNode Text(string text);

        IExpectUrlMethodChainNode Url(string url);
        IExpectUrlMethodChainNode Url(Func<Uri, bool> predicate);

        IExpectClassMethodChainNode Class(string className);
        
        IExpectCountMethodChainNode Count(int count);
        
        IExpectExistsMethodChainNode Exists(string selector);
        IExpectExistsMethodChainNode Exists(IDomElement element);
        
        IExpectValueMethodChainNode Value(int value);
        IExpectValueMethodChainNode Value(string value);
    }
}