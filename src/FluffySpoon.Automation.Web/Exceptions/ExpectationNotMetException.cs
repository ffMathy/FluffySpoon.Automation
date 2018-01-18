using FluffySpoon.Automation.Web.Fluent.Expect;
using System;

namespace FluffySpoon.Automation.Web.Exceptions
{
	public class ExpectationNotMetException : ApplicationException
	{
		public string ExpectationName { get; private set; }

		public static ExpectationNotMetException FromMethodChainNode(
			IBaseExpectMethodChainNode node,
			string message)
		{
			var expectationName = GetExpectationName(node);

			var finalMessage = $@"The expectation ""{expectationName}"" failed.";
			if (message != null)
				finalMessage += $" {message}";

			return new ExpectationNotMetException(expectationName, finalMessage);
		}

		private static string GetExpectationName(IBaseExpectMethodChainNode node)
		{
			var type = node.GetType();

			var name = type.Name;
			name = name.Substring("Expect".Length);
			name = name.Substring(0, name.IndexOf("MethodChainNode"));

			return name;
		}

		private ExpectationNotMetException(string expectationName, string message)
			: base(message)
		{
			ExpectationName = expectationName;
		}
	}
}
