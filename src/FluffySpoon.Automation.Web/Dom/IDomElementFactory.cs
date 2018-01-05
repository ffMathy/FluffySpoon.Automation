namespace FluffySpoon.Automation.Web.Dom
{
	public interface IDomElementFactory
	{
		IDomElement Create(string cssSelector);
	}
}