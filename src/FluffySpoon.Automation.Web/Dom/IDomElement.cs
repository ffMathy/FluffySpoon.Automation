namespace FluffySpoon.Automation.Web.Dom
{
	public interface IDomElement
	{
		string CssSelector { get; }
		string TextContent { get; }
		string Value { get; }

		IDomAttributes Attributes { get; }
		IDomRectangle BoundingClientRectangle { get; }
	}
}