namespace FluffySpoon.Automation.Web.Dom
{
    public class DomCoordinate : IDomCoordinate
    {
        public decimal X { get; }
        public decimal Y { get; }

        public DomCoordinate(
            decimal x,
            decimal y)
        {
            X = x;
            Y = y;
        }
    }

    public class DomRectangle : IDomRectangle
	{
		public DomRectangle(
            decimal left,
            decimal top,
            decimal right,
            decimal bottom)
		{
			Left = left;
			Top = top;
			Right = right;
			Bottom = bottom;
		}

		public decimal Left { get; }
		public decimal Top { get; }

		public decimal Right { get; }
		public decimal Bottom { get; }

		public decimal X => Left;
		public decimal Y => Top;

		public decimal Width => Right - Left;
		public decimal Height => Bottom - Top;

        public IDomCoordinate RelativeCenter => new DomCoordinate(
            Width / 2,
            Height / 2);

        public IDomCoordinate Center => new DomCoordinate(
            Left + Width / 2,
            Top + Height / 2);
    }
}
