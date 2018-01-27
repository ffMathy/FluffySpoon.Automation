using System;
using FluffySpoon.Automation.Web.Fluent.Click;
using FluffySpoon.Automation.Web.Fluent.DoubleClick;
using FluffySpoon.Automation.Web.Fluent.Drag;
using FluffySpoon.Automation.Web.Fluent.Enter;
using FluffySpoon.Automation.Web.Fluent.Expect;
using FluffySpoon.Automation.Web.Fluent.Expect.Root;
using FluffySpoon.Automation.Web.Fluent.Find;
using FluffySpoon.Automation.Web.Fluent.Focus;
using FluffySpoon.Automation.Web.Fluent.Hover;
using FluffySpoon.Automation.Web.Fluent.Open;
using FluffySpoon.Automation.Web.Fluent.RightClick;
using FluffySpoon.Automation.Web.Fluent.Select;
using FluffySpoon.Automation.Web.Fluent.TakeScreenshot;
using FluffySpoon.Automation.Web.Fluent.Targets.In;
using FluffySpoon.Automation.Web.Fluent.Targets.Of;
using FluffySpoon.Automation.Web.Fluent.Targets.On;
using FluffySpoon.Automation.Web.Fluent.Upload;
using FluffySpoon.Automation.Web.Fluent.Wait;

namespace FluffySpoon.Automation.Web.Fluent.Root
{
	public interface IMethodChainRoot: IAwaitable
	{
		IDomElementOfTargetMethodChainNode<IBaseMethodChainNode, ITakeScreenshotOfTargetMethodChainNode> TakeScreenshot { get; }
		IMouseOnTargetsMethodChainNode<IBaseMethodChainNode, IClickOnTargetsMethodChainNode> Click { get; }
		IDoubleClickMethodChainNode DoubleClick { get; }
		IRightClickMethodChainNode RightClick { get; }
		IHoverMethodChainNode Hover { get; }
		IDragMethodChainNode Drag { get; }
		IFocusMethodChainNode Focus { get; }
		ISelectMethodChainNode Select { get; }
		IExpectMethodChainRoot Expect { get; }

		IMethodChainRoot Wait(TimeSpan time);
		IMethodChainRoot Wait(int milliseconds);

		//TODO: should be called WaitUntil, and should share an Expect chain so that it can wait until specific expects are done
		IMethodChainRoot Wait(Func<bool> predicate);

		IOpenMethodChainNode Open(string uri);
        IOpenMethodChainNode Open(Uri uri);
		
		IMethodChainRoot Find(string selector);

		IUploadMethodChainNode Upload(string filePath);

        IDomElementInTargetsMethodChainNode<IBaseMethodChainNode, IEnterInTargetMethodChainNode> Enter(string text);
    }
}