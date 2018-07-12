using FluffySpoon.Automation.Web.Fluent.Expect;
using System;

namespace FluffySpoon.Automation.Web.Exceptions
{
	public class ExpectationNotMetException : ApplicationException
	{
		public string ExpectationName { get; }

		public static ExpectationNotMetException FromMethodChainNode(
			IBaseExpectMethodChainNode node,
			string userAgent,
			string message)
		{
			var expectationName = GetExpectationName(node);

			var finalMessage = $@"The expectation ""{expectationName}"" failed on {userAgent}.";
			if (message != null)
				finalMessage += $" {message}";

			return new ExpectationNotMetException(expectationName, finalMessage);
		}

		private static string GetExpectationName(IBaseExpectMethodChainNode node)
		{
			var type = node.GetType();

			var name = type.Name;
			name = name.Substring("Expect".Length);
			name = name.Substring(0, name.IndexOf("MethodChainNode", StringComparison.Ordinal));

			return name;
		}

		private ExpectationNotMetException(
			string expectationName,
			string message)
			: base(message)
		{
			ExpectationName = expectationName;
		}
	}
}
