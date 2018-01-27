namespace FluffySpoon.Automation.Web.Dom
{
	public interface IDomElement
	{
		string CssSelector { get; }
		IDomAttributes Attributes { get; }
		IDomRectangle BoundingClientRectangle { get; }
	}
}