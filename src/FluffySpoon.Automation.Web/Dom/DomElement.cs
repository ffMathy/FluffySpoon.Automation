namespace FluffySpoon.Automation.Web.Dom
{
	public class DomElement : IDomElement
	{
		public string CssSelector { get; }
		public string TextContent { get; }
		public string Value { get; }

		public IDomAttributes Attributes { get; }
		public IDomRectangle BoundingClientRectangle { get; }

		public DomElement(
			string cssSelector,
			string textContent,
			string value,
			IDomRectangle boundingClientRectangle,
			IDomAttributes attributes)
		{
			CssSelector = cssSelector;
			TextContent = textContent;
			Value = value;

			BoundingClientRectangle = boundingClientRectangle;
			Attributes = attributes;
		}
	}
}
