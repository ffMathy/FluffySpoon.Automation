using System.Collections.Generic;
using FluffySpoon.Automation.Web.Dom;
using FluffySpoon.Automation.Web.Fluent.Expect.Class;
using FluffySpoon.Automation.Web.Fluent.Expect.Count;
using FluffySpoon.Automation.Web.Fluent.Expect.Exists;
using FluffySpoon.Automation.Web.Fluent.Expect.Text;
using FluffySpoon.Automation.Web.Fluent.Expect.Uri;
using FluffySpoon.Automation.Web.Fluent.Expect.Value;
using FluffySpoon.Automation.Web.Fluent.Targets.In;
using FluffySpoon.Automation.Web.Fluent.Targets.Of;
using SystemUri = System.Uri;

namespace FluffySpoon.Automation.Web.Fluent.Expect.Root
{
    public interface IExpectMethodChainRoot: IBaseMethodChainNode
    {
		IDomElementInTargetsMethodChainNode<IBaseMethodChainNode, IExpectTextInTargetsMethodChainNode> Text(string text);

        IExpectUriMethodChainNode Uri(string uri);
        IExpectUriMethodChainNode Uri(SystemUri uri);

        IDomElementOfTargetsMethodChainNode<IBaseMethodChainNode, IExpectClassesOfTargetsMethodChainNode> Classes(params string[] classNames);

		IDomElementOfTargetsMethodChainNode<IBaseMethodChainNode, IExpectCountOfTargetsMethodChainNode> Count(int count);

		IExpectExistsMethodChainNode Exists(string selector);
        IExpectExistsMethodChainNode Exists(IDomElement element);
		IExpectExistsMethodChainNode Exists(IReadOnlyList<IDomElement> elements);

		IDomElementInTargetsMethodChainNode<IBaseMethodChainNode, IExpectValueInTargetsMethodChainNode> Value(string value);
    }
}