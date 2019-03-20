namespace FluffySpoon.Automation.Web.Dom
{
    public interface IDomElement
    {
        string CssSelector { get; }
        string TextContent { get; }
        string Value { get; }
        string TagName { get; }

        int ClientLeft { get; }
        int ClientTop { get; }
        int ClientWidth { get; }
        int ClientHeight { get; }

        IDomRectangle BoundingClientRectangle { get; }
        IDomAttributes Attributes { get; }
        IDomStyle ComputedStyle { get; }
        System.DateTime UpdatedAt { get; }
    }
}