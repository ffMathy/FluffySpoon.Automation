namespace FluffySpoon.Automation.Web.Dom
{
    public interface IDomCoordinate
    {
        decimal X { get; }
        decimal Y { get; }
    }

	public interface IDomRectangle : IDomCoordinate
	{
		decimal Left { get; }
        decimal Top { get; }

        decimal Right { get; }
        decimal Bottom { get; }

        decimal Width { get; }
        decimal Height { get; }

        IDomCoordinate RelativeCenter { get; }
        IDomCoordinate Center { get; }
    }
}
