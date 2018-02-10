namespace FluffySpoon.Automation.Web.Dom
{
	public interface IDomRectangle
	{
		double Left { get; }
		double Top { get; }

		double Right { get; }
		double Bottom { get; }

		double X { get; }
		double Y { get; }

		double Width { get; }
		double Height { get; }
	}
}
