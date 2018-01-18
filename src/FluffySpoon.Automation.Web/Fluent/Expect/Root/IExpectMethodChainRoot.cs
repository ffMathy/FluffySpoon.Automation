using System;
using System.Collections.Generic;
using FluffySpoon.Automation.Web.Dom;
using FluffySpoon.Automation.Web.Fluent.Expect.Class;
using FluffySpoon.Automation.Web.Fluent.Expect.Count;
using FluffySpoon.Automation.Web.Fluent.Expect.Exists;
using FluffySpoon.Automation.Web.Fluent.Expect.Text;
using FluffySpoon.Automation.Web.Fluent.Expect.Uri;
using FluffySpoon.Automation.Web.Fluent.Expect.Value;

using SystemUri = System.Uri;

namespace FluffySpoon.Automation.Web.Fluent.Expect.Root
{
    public interface IExpectMethodChainRoot: IBaseMethodChainNode
    {
        IExpectTextMethodChainNode Text(string text);

        IExpectUriMethodChainNode Uri(string uri);
        IExpectUriMethodChainNode Uri(SystemUri uri);
		IExpectUriMethodChainNode Uri(Func<SystemUri, bool> predicate);

        IExpectClassMethodChainNode Class(string className);
        IExpectClassMethodChainNode Class(Func<string> classNamePredicate);

		IExpectCountMethodChainNode Count(int count);

		IExpectExistsMethodChainNode Exists(string selector);
        IExpectExistsMethodChainNode Exists(IDomElement element);
        IExpectExistsMethodChainNode Exists(IReadOnlyCollection<IDomElement> elements);

		IExpectValueMethodChainNode Value(int value);
        IExpectValueMethodChainNode Value(string value);
    }
}