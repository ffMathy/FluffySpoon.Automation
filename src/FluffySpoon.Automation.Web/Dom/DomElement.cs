namespace FluffySpoon.Automation.Web.Dom
{
	public class DomElement : IDomElement
	{
		public string CssSelector { get; }
		public IDomAttributes Attributes { get; }
		public IDomRectangle BoundingClientRectangle { get; }

		public DomElement(
			string cssSelector,
			IDomRectangle boundingClientRectangle,
			IDomAttributes attributes)
		{
			CssSelector = cssSelector;
			BoundingClientRectangle = boundingClientRectangle;
			Attributes = attributes;
		}
	}
}
