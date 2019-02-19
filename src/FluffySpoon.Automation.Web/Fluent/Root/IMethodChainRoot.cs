using System;
using System.Threading.Tasks;
using FluffySpoon.Automation.Web.Fluent.Click;
using FluffySpoon.Automation.Web.Fluent.DoubleClick;
using FluffySpoon.Automation.Web.Fluent.Drag;
using FluffySpoon.Automation.Web.Fluent.Enter;
using FluffySpoon.Automation.Web.Fluent.Expect.Root;
using FluffySpoon.Automation.Web.Fluent.Find;
using FluffySpoon.Automation.Web.Fluent.Focus;
using FluffySpoon.Automation.Web.Fluent.Hover;
using FluffySpoon.Automation.Web.Fluent.Open;
using FluffySpoon.Automation.Web.Fluent.RightClick;
using FluffySpoon.Automation.Web.Fluent.Select;
using FluffySpoon.Automation.Web.Fluent.TakeScreenshot;
using FluffySpoon.Automation.Web.Fluent.Targets.From;
using FluffySpoon.Automation.Web.Fluent.Targets.In;
using FluffySpoon.Automation.Web.Fluent.Targets.Of;
using FluffySpoon.Automation.Web.Fluent.Targets.On;
using FluffySpoon.Automation.Web.Fluent.Targets.To;
using FluffySpoon.Automation.Web.Fluent.Upload;
using FluffySpoon.Automation.Web.Fluent.Wait;

namespace FluffySpoon.Automation.Web.Fluent.Root
{
	public interface IMethodChainRoot
	{
		IDomElementOfTargetMethodChainNode<IBaseMethodChainNode, ITakeScreenshotOfTargetMethodChainNode> TakeScreenshot { get; }

		IMouseOnTargetsMethodChainNode<IBaseMethodChainNode, IClickOnTargetsMethodChainNode> Click { get; }
		IMouseOnTargetsMethodChainNode<IBaseMethodChainNode, IDoubleClickOnTargetsMethodChainNode> DoubleClick { get; }
		IMouseOnTargetsMethodChainNode<IBaseMethodChainNode, IRightClickOnTargetsMethodChainNode> RightClick { get; }

		IMouseOnTargetMethodChainNode<IBaseMethodChainNode, IHoverOnTargetMethodChainNode> Hover { get; }
		IMouseFromTargetMethodChainNode<IBaseMethodChainNode, IMouseToTargetMethodChainNode<IBaseMethodChainNode, IDragFromTargetToTargetMethodChainNode>> Drag { get; }
		IDomElementOnTargetMethodChainNode<IBaseMethodChainNode, IFocusOnTargetMethodChainNode> Focus { get; }
		ISelectMethodChainNode Select { get; }
		IExpectMethodChainRoot Expect { get; }

		IWaitMethodChainNode Wait(TimeSpan time);
		IWaitMethodChainNode Wait(int milliseconds);
		IWaitMethodChainNode Wait(Func<bool> predicate);
		IWaitMethodChainNode Wait(Func<Task<bool>> predicate);
		IWaitMethodChainNode Wait(Action<IExpectMethodChainRoot> predicate);

		IFindMethodChainNode Find(string selector);

		IOpenMethodChainNode Open(string uri);
        IOpenMethodChainNode Open(Uri uri);

		IUploadMethodChainNode Upload(string filePath);

        IDomElementInTargetsMethodChainNode<IBaseMethodChainNode, IEnterInTargetMethodChainNode> Enter(string text);
    }
}