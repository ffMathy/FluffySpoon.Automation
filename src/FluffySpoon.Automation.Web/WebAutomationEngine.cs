using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using FluffySpoon.Automation.Web.Dom;
using FluffySpoon.Automation.Web.Fluent;
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

namespace FluffySpoon.Automation.Web
{
    class WebAutomationEngine : IWebAutomationEngine
    {
        private readonly IMethodChainContextFactory _methodChainContextFactory;
        private readonly ICollection<IMethodChainContext> _pendingQueues;

        public WebAutomationEngine(
			IMethodChainContextFactory methodChainContextFactory)
        {
            _pendingQueues = new HashSet<IMethodChainContext>();

            _methodChainContextFactory = methodChainContextFactory;
        }

        public TaskAwaiter GetAwaiter()
        {
            return Task.WhenAll(_pendingQueues.Select(x => x.RunAllAsync())).GetAwaiter();
        }

        public Task ExecuteAsync(IWebAutomationFrameworkInstance framework)
        {
            return Task.CompletedTask;
        }

        public IOpenMethodChainNode Open(string uri)
        {
            return StartNewSession(new OpenMethodChainNode(uri));
        }

        public IOpenMethodChainNode Open(Uri uri)
        {
            return Open(uri.ToString());
        }

        public IEnterMethodChainNode Enter(string text)
        {
			return StartNewSession(new EnterMethodChainNode(text));
        }

		public IFindMethodChainNode Find(string selector)
		{
			throw new NotImplementedException();
		}

		public ITakeScreenshotMethodChainNode TakeScreenshot()
		{
			throw new NotImplementedException();
		}

		public ITakeScreenshotMethodChainNode TakeScreenshot(string selector)
		{
			throw new NotImplementedException();
		}

		public ITakeScreenshotMethodChainNode TakeScreenshot(IDomElement element)
		{
			throw new NotImplementedException();
		}

		public IUploadMethodChainNode Upload(string selector, string filePath)
		{
			throw new NotImplementedException();
		}

		public IUploadMethodChainNode Upload(IDomElement element, string filePath)
		{
			throw new NotImplementedException();
		}

		public IClickMethodChainNode Click(string selector)
		{
			throw new NotImplementedException();
		}

		public IClickMethodChainNode Click(string selector, int relativeX, int relativeY)
		{
			throw new NotImplementedException();
		}

		public IClickMethodChainNode Click(int x, int y)
		{
			throw new NotImplementedException();
		}

		public IClickMethodChainNode Click(IDomElement element)
		{
			throw new NotImplementedException();
		}

		public IClickMethodChainNode Click(IDomElement element, int relativeX, int relativeY)
		{
			throw new NotImplementedException();
		}

		public IDoubleClickMethodChainNode DoubleClick(string selector)
		{
			throw new NotImplementedException();
		}

		public IDoubleClickMethodChainNode DoubleClick(string selector, int relativeX, int relativeY)
		{
			throw new NotImplementedException();
		}

		public IDoubleClickMethodChainNode DoubleClick(int x, int y)
		{
			throw new NotImplementedException();
		}

		public IDoubleClickMethodChainNode DoubleClick(IDomElement element)
		{
			throw new NotImplementedException();
		}

		public IDoubleClickMethodChainNode DoubleClick(IDomElement element, int relativeX, int relativeY)
		{
			throw new NotImplementedException();
		}

		public IRightClickMethodChainNode RightClick(string selector)
		{
			throw new NotImplementedException();
		}

		public IRightClickMethodChainNode RightClick(string selector, int relativeX, int relativeY)
		{
			throw new NotImplementedException();
		}

		public IRightClickMethodChainNode RightClick(int x, int y)
		{
			throw new NotImplementedException();
		}

		public IRightClickMethodChainNode RightClick(IDomElement element)
		{
			throw new NotImplementedException();
		}

		public IRightClickMethodChainNode RightClick(IDomElement element, int relativeX, int relativeY)
		{
			throw new NotImplementedException();
		}

		public IHoverMethodChainNode Hover(string selector)
		{
			throw new NotImplementedException();
		}

		public IHoverMethodChainNode Hover(string selector, int relativeX, int relativeY)
		{
			throw new NotImplementedException();
		}

		public IHoverMethodChainNode Hover(int x, int y)
		{
			throw new NotImplementedException();
		}

		public IHoverMethodChainNode Hover(IDomElement element)
		{
			throw new NotImplementedException();
		}

		public IHoverMethodChainNode Hover(IDomElement element, int relativeX, int relativeY)
		{
			throw new NotImplementedException();
		}

		public IDragMethodChainNode Drag(string selector)
		{
			throw new NotImplementedException();
		}

		public IDragMethodChainNode Drag(string selector, int relativeX, int relativeY)
		{
			throw new NotImplementedException();
		}

		public IDragMethodChainNode Drag(int x, int y)
		{
			throw new NotImplementedException();
		}

		public IDragMethodChainNode Drag(IDomElement element)
		{
			throw new NotImplementedException();
		}

		public IDragMethodChainNode Drag(IDomElement element, int relativeX, int relativeY)
		{
			throw new NotImplementedException();
		}

		public IFocusMethodChainNode Focus(string selector)
		{
			throw new NotImplementedException();
		}

		public IFocusMethodChainNode Focus(IDomElement element)
		{
			throw new NotImplementedException();
		}

		public ISelectMethodChainNode Select(string value)
		{
			throw new NotImplementedException();
		}

		public ISelectMethodChainNode Select(int index)
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

		public IExpectMethodChainNode Expect { get; }

		public IMethodChainContext MethodChainContext { set => throw new NotImplementedException("The method chain context can't be set on the web automation engine."); }

		private TMethodChainNode StartNewSession<TMethodChainNode>(TMethodChainNode nodeToStart) where TMethodChainNode : IBaseMethodChainNode {
			return CreateNewQueue().Enqueue(nodeToStart);
		}

		private IMethodChainContext CreateNewQueue()
		{
			var methodChainQueue = _methodChainContextFactory.Create();
			_pendingQueues.Add(methodChainQueue);

			return methodChainQueue;
		}
	}
}
