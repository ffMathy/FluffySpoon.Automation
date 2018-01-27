﻿using System;
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
		public IExpectMethodChainRoot Expect => 
			MethodChainContext.Enqueue(new ExpectMethodChainRoot<IBaseMethodChainNode>());

		public IDomElementOfTargetMethodChainNode<IBaseMethodChainNode, ITakeScreenshotOfTargetMethodChainNode> TakeScreenshot =>
			MethodChainContext.Enqueue(new TakeScreenshotChainNode());

		public IMouseOnTargetsMethodChainNode<IBaseMethodChainNode, IClickOnTargetsMethodChainNode> Click => 
			MethodChainContext.Enqueue(new ClickMethodChainNode());
		public IMouseOnTargetsMethodChainNode<IBaseMethodChainNode, IDoubleClickOnTargetsMethodChainNode> DoubleClick =>
			MethodChainContext.Enqueue(new DoubleClickMethodChainNode());
		public IMouseOnTargetsMethodChainNode<IBaseMethodChainNode, IRightClickOnTargetsMethodChainNode> RightClick =>
			MethodChainContext.Enqueue(new RightClickMethodChainNode());

		public IMouseOnTargetMethodChainNode<IBaseMethodChainNode, IHoverOnTargetMethodChainNode> Hover =>
			MethodChainContext.Enqueue(new HoverMethodChainNode());

		public IDragMethodChainNode Drag => throw new NotImplementedException();
		public IFocusMethodChainNode Focus => throw new NotImplementedException();
		public ISelectMethodChainNode Select => throw new NotImplementedException();

		public IDomElementInTargetsMethodChainNode<IBaseMethodChainNode, IEnterInTargetMethodChainNode> Enter(string text) =>
			MethodChainContext.Enqueue(new EnterMethodChainNode(text));

		public IMethodChainRoot Find(string selector) =>
			MethodChainContext.Enqueue(new FindMethodChainNode(selector));

		public IOpenMethodChainNode Open(string uri) => 
			MethodChainContext.Enqueue(new OpenMethodChainNode(uri));
		public IOpenMethodChainNode Open(Uri uri) => 
			Open(uri.ToString());

		public IUploadMethodChainNode Upload(string filePath)
		{
			throw new NotImplementedException();
		}

		public IMethodChainRoot Wait(TimeSpan timespan) => MethodChainContext.Enqueue(new WaitMethodChainNode(timespan));
		public IMethodChainRoot Wait(int milliseconds) => Wait(TimeSpan.FromMilliseconds(milliseconds));
		public IMethodChainRoot Wait(Func<bool> predicate)
		{
			throw new NotImplementedException();
		}
	}
}
