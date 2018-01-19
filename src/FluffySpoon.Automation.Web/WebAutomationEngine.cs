using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using FluffySpoon.Automation.Web.Dom;
using FluffySpoon.Automation.Web.Fluent;
using FluffySpoon.Automation.Web.Fluent.Click;
using FluffySpoon.Automation.Web.Fluent.Context;
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
using FluffySpoon.Automation.Web.Fluent.Root;
using FluffySpoon.Automation.Web.Fluent.Select;
using FluffySpoon.Automation.Web.Fluent.TakeScreenshot;
using FluffySpoon.Automation.Web.Fluent.Targets.In;
using FluffySpoon.Automation.Web.Fluent.Targets.Of;
using FluffySpoon.Automation.Web.Fluent.Targets.On;
using FluffySpoon.Automation.Web.Fluent.Upload;
using FluffySpoon.Automation.Web.Fluent.Wait;

namespace FluffySpoon.Automation.Web
{
	class WebAutomationEngine : IWebAutomationEngine
    {
        private readonly IMethodChainContextFactory _methodChainContextFactory;
		private readonly IDomSelectorStrategy _domSelectorStrategy;

		private readonly ICollection<IMethodChainContext> _pendingQueues;

		private bool _isInitialized;
		private bool _isInitializing;

		public WebAutomationEngine(
			IMethodChainContextFactory methodChainContextFactory,
			IDomSelectorStrategy domSelectorStrategy)
        {
            _pendingQueues = new HashSet<IMethodChainContext>();

            _methodChainContextFactory = methodChainContextFactory;
			_domSelectorStrategy = domSelectorStrategy;
        }

        public TaskAwaiter GetAwaiter()
        {
            return Task.WhenAll(_pendingQueues.Select(x => x.RunAllAsync())).GetAwaiter();
        }
		
		public async Task InitializeAsync()
		{
			if (_isInitializing || _isInitialized)
				throw new InvalidOperationException("Can't call initialize twice.");
				
			_isInitializing = true;
			await _domSelectorStrategy.InitializeAsync();
			_isInitialized = true;
			_isInitializing = false;
		}

		public IExpectMethodChainRoot Expect => StartNewSession().Expect;
		public IDomElementOfTargetMethodChainNode<IBaseMethodChainNode, ITakeScreenshotOfTargetMethodChainNode> TakeScreenshot => StartNewSession().TakeScreenshot;
		public IMouseOnTargetsMethodChainNode<IBaseMethodChainNode, IClickOnTargetMethodChainNode> Click => StartNewSession().Click;
		public IDoubleClickMethodChainNode DoubleClick => StartNewSession().DoubleClick;
		public IRightClickMethodChainNode RightClick => StartNewSession().RightClick;
		public IHoverMethodChainNode Hover => StartNewSession().Hover;
		public IDragMethodChainNode Drag => StartNewSession().Drag;
		public IFocusMethodChainNode Focus => StartNewSession().Focus;
		public ISelectMethodChainNode Select => StartNewSession().Select;

		public IWaitMethodChainNode Wait(TimeSpan time) => StartNewSession().Wait(time);
		public IWaitMethodChainNode Wait(int milliseconds) => StartNewSession().Wait(milliseconds);
		public IWaitMethodChainNode Wait(Func<bool> predicate) => StartNewSession().Wait(predicate);

		public IOpenMethodChainNode Open(string uri) => StartNewSession().Open(uri);
		public IOpenMethodChainNode Open(Uri uri) => StartNewSession().Open(uri);

		public IFindMethodChainNode Find(string selector) => StartNewSession().Find(selector);

		public IUploadMethodChainNode Upload(string filePath) => StartNewSession().Upload(filePath);

		public IDomElementInTargetsMethodChainNode<IBaseMethodChainNode, IEnterInTargetMethodChainNode> Enter(string text) => StartNewSession().Enter(text);

		private IMethodChainRoot StartNewSession()
		{
			if (_isInitializing)
				throw new InvalidOperationException("The web automation engine is not done initializing yet. Remember to await the " + nameof(InitializeAsync) + " call.");

			if (!_isInitialized)
				throw new InvalidOperationException("Can't automate anything when the web engine is not initialized yet. Call " + nameof(InitializeAsync) + " first.");

			return new MethodChainRoot()
			{
				MethodChainContext = CreateNewQueue()
			};
		}

		private IMethodChainContext CreateNewQueue()
		{
			var methodChainQueue = _methodChainContextFactory.Create();
			_pendingQueues.Add(methodChainQueue);

			return methodChainQueue;
		}
	}
}
