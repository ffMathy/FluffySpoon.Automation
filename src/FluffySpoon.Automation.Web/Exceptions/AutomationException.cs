using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace FluffySpoon.Automation.Web.Exceptions
{
	public class AutomationException : ApplicationException
	{
		public AutomationException()
		{
		}

		public AutomationException(string message) : base(message)
		{
		}

		public AutomationException(string message, Exception innerException) : base(message, innerException)
		{
		}

		protected AutomationException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}
	}
}
