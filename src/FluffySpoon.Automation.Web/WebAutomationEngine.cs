using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using FluffySpoon.Automation.Web.Fluent;

namespace FluffySpoon.Automation.Web
{
    public class WebAutomationEngine : IWebAutomationEngine
    {
        private readonly IMethodChainQueueFactory _methodChainQueueFactory;
        private readonly ICollection<IMethodChainQueue> _pendingQueues;

        private IEnumerable<IWebAutomationTechnology> _technologies;

        public WebAutomationEngine() : this(
            new MethodChainQueueFactory())
        {
        }

        public WebAutomationEngine(IMethodChainQueueFactory methodChainQueueFactory)
        {
            _pendingQueues = new HashSet<IMethodChainQueue>();

            _methodChainQueueFactory = methodChainQueueFactory;
        }

        public void Configure(IWebAutomationTechnology technology)
        {
            Configure(new[] { technology });
        }

        public void Configure(IEnumerable<IWebAutomationTechnology> technologies)
        {
            _technologies = technologies;
        }

        public TaskAwaiter GetAwaiter()
        {
            return Task.WhenAll(_pendingQueues.Select(x => x.RunAllAsync())).GetAwaiter();
        }

        public Task ExecuteAsync(IWebAutomationTechnology technology)
        {
            return Task.CompletedTask;
        }

        public IOpenMethodChainNode Open(string uri)
        {
            var methodChainQueue = _methodChainQueueFactory.Create();
            return methodChainQueue
                .Enqueue(new OpenMethodChainNode(
                    ));
        }

        public IOpenMethodChainNode Open(Uri uri)
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

        public IEnterMethodChainNode Enter(string text)
        {
            throw new NotImplementedException();
        }

        public IExpectMethodChainNode Expect { get; }
    }
}
