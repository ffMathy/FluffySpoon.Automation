using System;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using FluffySpoon.Automation.Web.Exceptions;
using FluffySpoon.Automation.Web.Fluent.Click;
using FluffySpoon.Automation.Web.Fluent.Context;
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
	abstract class MethodChainRoot<TParentMethodChainNode> :
		BaseMethodChainNode<TParentMethodChainNode>,
		IMethodChainRoot,
		IBaseMethodChainNode,
		IAwaitable
		where TParentMethodChainNode : IBaseMethodChainNode
	{
		public IExpectMethodChainRoot Expect =>
			MethodChainContext.Enqueue(new ExpectMethodChainEntryPoint());

		public IDomElementOfTargetsMethodChainNode<IBaseMethodChainNode, ITakeScreenshotOfTargetsMethodChainNode> TakeScreenshot =>
			MethodChainContext.Enqueue(new TakeScreenshotChainNode());

		public IMouseOnTargetsMethodChainNode<IBaseMethodChainNode, IClickOnTargetsMethodChainNode> Click =>
			MethodChainContext.Enqueue(new ClickMethodChainNode());
		public IMouseOnTargetsMethodChainNode<IBaseMethodChainNode, IDoubleClickOnTargetsMethodChainNode> DoubleClick =>
			MethodChainContext.Enqueue(new DoubleClickMethodChainNode());
		public IMouseOnTargetsMethodChainNode<IBaseMethodChainNode, IRightClickOnTargetsMethodChainNode> RightClick =>
			MethodChainContext.Enqueue(new RightClickMethodChainNode());

		public IMouseOnTargetMethodChainNode<IBaseMethodChainNode, IHoverOnTargetMethodChainNode> Hover =>
			MethodChainContext.Enqueue(new HoverMethodChainNode());

		public IMouseFromTargetMethodChainNode<IBaseMethodChainNode, IMouseToTargetMethodChainNode<IBaseMethodChainNode, IDragFromTargetToTargetMethodChainNode>> Drag =>
			MethodChainContext.Enqueue(new DragMethodChainNode());

		public IDomElementOnTargetMethodChainNode<IBaseMethodChainNode, IFocusOnTargetMethodChainNode> Focus =>
			MethodChainContext.Enqueue(new FocusMethodChainNode());

		public ISelectMethodChainNode Select =>
			MethodChainContext.Enqueue(new SelectMethodChainNode());

		public IDomElementInTargetsMethodChainNode<IBaseMethodChainNode, IEnterInTargetMethodChainNode> Enter(string text) =>
			MethodChainContext.Enqueue(new EnterMethodChainNode(text));

		public IFindMethodChainNode Find(string selector) => 
			MethodChainContext.Enqueue(new FindMethodChainNode(selector));

		public IOpenMethodChainNode Open(string uri) =>
			MethodChainContext.Enqueue(new OpenMethodChainNode(uri));
		public IOpenMethodChainNode Open(Uri uri) =>
			Open(uri.ToString());

		public IUploadMethodChainNode Upload(string filePath)
		{
			throw new NotImplementedException();
		}

		public IWaitMethodChainNode Wait(TimeSpan timespan)
		{
			DateTime? startTime = null;
			return Wait(() =>
			{
				if (startTime == null)
					startTime = DateTime.UtcNow;

				return DateTime.UtcNow - startTime > timespan;
			});
		}
		public IWaitMethodChainNode Wait(int milliseconds) => Wait(TimeSpan.FromMilliseconds(milliseconds));
		public IWaitMethodChainNode Wait(Func<Task<bool>> predicate) => MethodChainContext.Enqueue(new WaitMethodChainNode(predicate));
		public IWaitMethodChainNode Wait(Func<bool> predicate)
		{
			return Wait(() => Task.Factory.StartNew(predicate));
		}
		public IWaitMethodChainNode Wait(Action<IExpectMethodChainRoot> predicate)
		{
			return Wait(async () =>
			{
				while(MethodChainContext.Frameworks.Any(x => x.IsNavigating))
					await Task.Delay(100);

				var methodChainContext = new MethodChainContext(MethodChainContext.Frameworks, MethodChainContext.AutomationEngine);
				var expectNode = methodChainContext.Enqueue(new ExpectMethodChainEntryPoint());

				predicate(expectNode);

				try
				{
					await methodChainContext.RunAllAsync();
					return true;
				}
				catch (Exception ex) when (ex.InnerException is ExpectationNotMetException)
				{
				}
				catch (ExpectationNotMetException)
				{
				}

                return false;
			});
		}

		public new TaskAwaiter GetAwaiter()
		{
			return MethodChainContext.GetAwaiter();
		}
	}
}
