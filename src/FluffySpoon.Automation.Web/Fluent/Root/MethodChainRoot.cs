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
	class MethodChainRoot : MethodChainRoot<IBaseMethodChainNode>
	{

	}

	class MethodChainRoot<TParentMethodChainNode>: 
		BaseMethodChainNode<TParentMethodChainNode>, 
		IMethodChainRoot, 
		IBaseMethodChainNode, 
		IAwaitable
		where TParentMethodChainNode : IBaseMethodChainNode
	{
		public IExpectMethodChainRoot Expect => MethodChainContext.Enqueue(new ExpectMethodChainRoot());
		public IDomElementOfTargetMethodChainNode<IBaseMethodChainNode, ITakeScreenshotOfTargetMethodChainNode> TakeScreenshot => throw new NotImplementedException();
		public IMouseOnTargetsMethodChainNode<IBaseMethodChainNode, IClickOnTargetMethodChainNode> Click => throw new NotImplementedException();
		public IDoubleClickMethodChainNode DoubleClick => throw new NotImplementedException();
		public IRightClickMethodChainNode RightClick => throw new NotImplementedException();
		public IHoverMethodChainNode Hover => throw new NotImplementedException();
		public IDragMethodChainNode Drag => throw new NotImplementedException();
		public IFocusMethodChainNode Focus => throw new NotImplementedException();
		public ISelectMethodChainNode Select => throw new NotImplementedException();

		public IDomElementInTargetsMethodChainNode<IBaseMethodChainNode, IEnterInTargetMethodChainNode> Enter(string text) => MethodChainContext.Enqueue(new EnterMethodChainNode(text));

		public IMethodChainRoot Find(string selector)
		{
			return MethodChainContext.Enqueue(new FindMethodChainNode(selector));
		}

		public IOpenMethodChainNode Open(string uri) => MethodChainContext.Enqueue(new OpenMethodChainNode(uri));
		public IOpenMethodChainNode Open(Uri uri) => Open(uri.ToString());

		public IUploadMethodChainNode Upload(string filePath)
		{
			throw new NotImplementedException();
		}

		public IWaitMethodChainNode Wait(TimeSpan time)
		{
			throw new NotImplementedException();
		}

		public IWaitMethodChainNode Wait(int milliseconds)
		{
			throw new NotImplementedException();
		}

		public IWaitMethodChainNode Wait(Func<bool> predicate)
		{
			throw new NotImplementedException();
		}
	}
}
