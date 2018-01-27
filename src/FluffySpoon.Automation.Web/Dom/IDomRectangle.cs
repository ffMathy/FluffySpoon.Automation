using System;
using System.Collections.Generic;
using System.Text;

namespace FluffySpoon.Automation.Web.Dom
{
	public interface IDomRectangle
	{
		double X { get; }
		double Y { get; }

		double Width { get; }
		double Height { get; }
	}
}
