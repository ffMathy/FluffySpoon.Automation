namespace FluffySpoon.Automation.Web.Dom
{
	public class DomElement : IDomElement
	{
		public string CssSelector { get; }
		public string TextContent { get; }
		public string InnerText { get; }
		public string Value { get; }

		public int ClientLeft { get; }
		public int ClientTop { get; }
		public int ClientWidth { get; }
		public int ClientHeight { get; }

		public IDomRectangle BoundingClientRectangle { get; }
		public IDomAttributes Attributes { get; }
		public IDomStyle ComputedStyle { get; }

		public DomElement(
			string cssSelector,
			string textContent,
			string innerText,
			string value,
			int clientLeft,
			int clientTop,
			int clientWidth,
			int clientHeight,
			IDomRectangle boundingClientRectangle,
			IDomAttributes attributes,
			IDomStyle computedStyle)
		{
			CssSelector = cssSelector;
			TextContent = textContent;
			InnerText = innerText;
			Value = value;

			ClientLeft = clientLeft;
			ClientTop = clientTop;
			ClientWidth = clientWidth;
			ClientHeight = clientHeight;

			BoundingClientRectangle = boundingClientRectangle;
			Attributes = attributes;
			ComputedStyle = computedStyle;
		}
	}
}
