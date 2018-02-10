namespace FluffySpoon.Automation.Web.Dom
{
	public class DomRectangle : IDomRectangle
	{
		public DomRectangle(
			double left,
			double top,
			double right,
			double bottom)
		{
			Left = left;
			Top = top;
			Right = right;
			Bottom = bottom;
		}

		public double Left { get; }
		public double Top { get; }

		public double Right { get; }
		public double Bottom { get; }

		public double X => Left;
		public double Y => Top;

		public double Width => Right - Left;
		public double Height => Bottom - Top;
	}
}
