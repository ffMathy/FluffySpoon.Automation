using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using FluffySpoon.Automation.Web.Dom;
using FluffySpoon.Automation.Web.Fluent.Expect.Class;
using FluffySpoon.Automation.Web.Fluent.Expect.Count;
using FluffySpoon.Automation.Web.Fluent.Expect.Exists;
using FluffySpoon.Automation.Web.Fluent.Expect.Text;
using FluffySpoon.Automation.Web.Fluent.Expect.Uri;
using FluffySpoon.Automation.Web.Fluent.Expect.Value;
using FluffySpoon.Automation.Web.Fluent.Targets.In;
using FluffySpoon.Automation.Web.Fluent.Targets.Of;

namespace FluffySpoon.Automation.Web.Fluent.Expect.Root
{
	abstract class ExpectMethodChainRoot<TParentMethodChainNode> : BaseMethodChainNode<TParentMethodChainNode>, IExpectMethodChainRoot
		where TParentMethodChainNode : IBaseMethodChainNode
	{
		public IDomElementOfTargetsMethodChainNode<IBaseMethodChainNode, IExpectClassesOfTargetsMethodChainNode> Classes(params string[] classNames)
		{
			return MethodChainContext.Enqueue(new ExpectClassesMethodChainNode(classNames));
		}

		public IDomElementOfTargetsMethodChainNode<IBaseMethodChainNode, IExpectCountOfTargetsMethodChainNode> Count(int count)
		{
			return MethodChainContext.Enqueue(new ExpectCountMethodChainNode(count));
		}

		public IExpectExistsMethodChainNode Exists(string selector)
		{
			return MethodChainContext.Enqueue(new ExpectExistsMethodChainNode(selector));
		}

		public IExpectExistsMethodChainNode Exists(IDomElement element)
		{
			return Exists(new[] { element });
		}

		public IExpectExistsMethodChainNode Exists(IReadOnlyList<IDomElement> elements)
		{
			return MethodChainContext.Enqueue(new ExpectExistsMethodChainNode(elements));
		}

		public IDomElementInTargetsMethodChainNode<IBaseMethodChainNode, IExpectTextInTargetsMethodChainNode> Text(string text)
		{
			return MethodChainContext.Enqueue(new ExpectTextMethodChainNode(text));
		}

		public IExpectUriMethodChainNode Uri(string uri)
		{
			throw new NotImplementedException();
		}

		public IExpectUriMethodChainNode Uri(System.Uri uri)
		{
			throw new NotImplementedException();
		}

		public IDomElementInTargetsMethodChainNode<IBaseMethodChainNode, IExpectValueInTargetsMethodChainNode> Value(string value)
		{
			return MethodChainContext.Enqueue(new ExpectValueMethodChainNode(value));
		}

		public new TaskAwaiter<IReadOnlyList<IDomElement>> GetAwaiter()
		{
			return MethodChainContext
				.RunAllAsync()
				.ContinueWith(
					t => {
						if(t.Exception != null) { 
							throw t.Exception.InnerExceptions.Count == 1 ? 
								t.Exception.InnerExceptions.Single() :
								t.Exception;
						}

						return Elements ?? Array.Empty<IDomElement>();
					},
					TaskUtilities.ContinuationOptions)
				.GetAwaiter();
		}
	}
}
