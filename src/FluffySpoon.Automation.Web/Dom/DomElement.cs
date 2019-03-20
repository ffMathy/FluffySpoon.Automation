using System;

namespace FluffySpoon.Automation.Web.Dom
{
    public class DomElement : IDomElement
    {
        public DateTime UpdatedAt { get; }

        public string CssSelector { get; }
        public string TextContent { get; }
        public string InnerText { get; }
        public string Value { get; }
        public string TagName { get; }

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
            string tagName,
            int clientLeft,
            int clientTop,
            int clientWidth,
            int clientHeight,
            IDomRectangle boundingClientRectangle,
            IDomAttributes attributes,
            IDomStyle computedStyle,
            DateTime updatedAt)
        {
            CssSelector = cssSelector;
            TextContent = textContent;
            InnerText = innerText;
            Value = value;
            TagName = tagName;

            ClientLeft = clientLeft;
            ClientTop = clientTop;
            ClientWidth = clientWidth;
            ClientHeight = clientHeight;

            BoundingClientRectangle = boundingClientRectangle;
            Attributes = attributes;
            ComputedStyle = computedStyle;

            UpdatedAt = updatedAt;
        }
    }
}
