using System;
using System.Collections.Generic;
using FluffySpoon.Automation.Web.Dom;
using FluffySpoon.Automation.Web.Fluent.Expect.Class;
using FluffySpoon.Automation.Web.Fluent.Expect.Count;
using FluffySpoon.Automation.Web.Fluent.Expect.Exists;
using FluffySpoon.Automation.Web.Fluent.Expect.Text;
using FluffySpoon.Automation.Web.Fluent.Expect.Uri;
using FluffySpoon.Automation.Web.Fluent.Expect.Value;

namespace FluffySpoon.Automation.Web.Fluent.Expect.Root
{
	class ExpectMethodChainRoot : BaseMethodChainNode, IExpectMethodChainRoot
	{
		public IExpectClassMethodChainNode Class(string className)
		{
			throw new NotImplementedException();
		}

		public IExpectClassMethodChainNode Class(Func<string> classNamePredicate)
		{
			throw new NotImplementedException();
		}

		public IExpectCountMethodChainNode Count(int count)
		{
			throw new NotImplementedException();
		}

		public IExpectExistsMethodChainNode Exists(string selector)
		{
			return MethodChainContext.Enqueue(new ExpectExistsMethodChainNode(selector));
		}

		public IExpectExistsMethodChainNode Exists(IDomElement element)
		{
			throw new NotImplementedException();
		}

		public IExpectExistsMethodChainNode Exists(IReadOnlyCollection<IDomElement> elements)
		{
			throw new NotImplementedException();
		}

		public IExpectTextMethodChainNode Text(string text)
		{
			throw new NotImplementedException();
		}

		public IExpectUriMethodChainNode Uri(string uri)
		{
			throw new NotImplementedException();
		}

		public IExpectUriMethodChainNode Uri(System.Uri uri)
		{
			throw new NotImplementedException();
		}

		public IExpectUriMethodChainNode Uri(Func<System.Uri, bool> predicate)
		{
			throw new NotImplementedException();
		}

		public IExpectValueMethodChainNode Value(int value)
		{
			throw new NotImplementedException();
		}

		public IExpectValueMethodChainNode Value(string value)
		{
			throw new NotImplementedException();
		}
	}
}
