using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using FluffySpoon.Automation.Web.Fluent;
using FluffySpoon.Automation.Web.Fluent.Enter;
using FluffySpoon.Automation.Web.Fluent.Expect;
using FluffySpoon.Automation.Web.Fluent.Open;
using FluffySpoon.Automation.Web.Fluent.Select;

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
            var methodChainQueue = CreateNewQueue();
            return methodChainQueue
                .Enqueue(new OpenMethodChainNode(
                    methodChainQueue,
                    uri));
        }

        public IOpenMethodChainNode Open(Uri uri)
        {
            return Open(uri.ToString());
        }

        public ISelectMethodChainNode Select(string value)
        {
            throw new NotImplementedException();
        }

        public ISelectMethodChainNode Select(int index)
        {
            throw new NotImplementedException();
        }

        public IEnterMethodChainNode Enter(string text)
        {
            var methodChainQueue = CreateNewQueue();
            return methodChainQueue
                .Enqueue(new EnterMethodChainNode(
                    methodChainQueue,
                    text));
        }

        public IExpectMethodChainNode Expect { get; }

        private IMethodChainContext CreateNewQueue()
        {
            var methodChainQueue = _methodChainContextFactory.Create();
            _pendingQueues.Add(methodChainQueue);

            return methodChainQueue;
        }
    }
}
