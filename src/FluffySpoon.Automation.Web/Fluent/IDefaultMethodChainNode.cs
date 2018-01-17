using System;
using FluffySpoon.Automation.Web.Dom;
using FluffySpoon.Automation.Web.Fluent.Click;
using FluffySpoon.Automation.Web.Fluent.DoubleClick;
using FluffySpoon.Automation.Web.Fluent.Drag;
using FluffySpoon.Automation.Web.Fluent.Enter;
using FluffySpoon.Automation.Web.Fluent.Expect;
using FluffySpoon.Automation.Web.Fluent.Find;
using FluffySpoon.Automation.Web.Fluent.Focus;
using FluffySpoon.Automation.Web.Fluent.Hover;
using FluffySpoon.Automation.Web.Fluent.Open;
using FluffySpoon.Automation.Web.Fluent.RightClick;
using FluffySpoon.Automation.Web.Fluent.Select;
using FluffySpoon.Automation.Web.Fluent.TakeScreenshot;
using FluffySpoon.Automation.Web.Fluent.Upload;
using FluffySpoon.Automation.Web.Fluent.Wait;

namespace FluffySpoon.Automation.Web.Fluent
{
    public interface IDefaultMethodChainNode: IBaseMethodChainNode
    {
        IOpenMethodChainNode Open(string uri);
        IOpenMethodChainNode Open(Uri uri);
		
		IFindMethodChainNode Find(string selector);
		
		ITakeScreenshotMethodChainNode TakeScreenshot();
		ITakeScreenshotMethodChainNode TakeScreenshot(string selector);
		ITakeScreenshotMethodChainNode TakeScreenshot(IDomElement element);

		IUploadMethodChainNode Upload(string selector, string filePath);
		IUploadMethodChainNode Upload(IDomElement element, string filePath);

		IClickMethodChainNode Click(string selector);
		IClickMethodChainNode Click(string selector, int relativeX, int relativeY);
		IClickMethodChainNode Click(int x, int y);
		IClickMethodChainNode Click(IDomElement element);
		IClickMethodChainNode Click(IDomElement element, int relativeX, int relativeY);

		IDoubleClickMethodChainNode DoubleClick(string selector);
		IDoubleClickMethodChainNode DoubleClick(string selector, int relativeX, int relativeY);
		IDoubleClickMethodChainNode DoubleClick(int x, int y);
		IDoubleClickMethodChainNode DoubleClick(IDomElement element);
		IDoubleClickMethodChainNode DoubleClick(IDomElement element, int relativeX, int relativeY);

		IRightClickMethodChainNode RightClick(string selector);
		IRightClickMethodChainNode RightClick(string selector, int relativeX, int relativeY);
		IRightClickMethodChainNode RightClick(int x, int y);
		IRightClickMethodChainNode RightClick(IDomElement element);
		IRightClickMethodChainNode RightClick(IDomElement element, int relativeX, int relativeY);

		IHoverMethodChainNode Hover(string selector);
		IHoverMethodChainNode Hover(string selector, int relativeX, int relativeY);
		IHoverMethodChainNode Hover(int x, int y);
		IHoverMethodChainNode Hover(IDomElement element);
		IHoverMethodChainNode Hover(IDomElement element, int relativeX, int relativeY);

		IDragMethodChainNode Drag(string selector);
		IDragMethodChainNode Drag(string selector, int relativeX, int relativeY);
		IDragMethodChainNode Drag(int x, int y);
		IDragMethodChainNode Drag(IDomElement element);
		IDragMethodChainNode Drag(IDomElement element, int relativeX, int relativeY);

		IFocusMethodChainNode Focus(string selector);
		IFocusMethodChainNode Focus(IDomElement element);

        ISelectMethodChainNode Select(string value);
        ISelectMethodChainNode Select(int index);

        IEnterMethodChainNode Enter(string text);

		IWaitMethodChainNode Wait(TimeSpan time);
		IWaitMethodChainNode Wait(int milliseconds);
		IWaitMethodChainNode Wait(Func<bool> predicate);

        IExpectMethodChainNode Expect { get; }
    }
}