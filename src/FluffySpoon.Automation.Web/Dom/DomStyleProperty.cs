namespace FluffySpoon.Automation.Web.Dom
{
	public class DomStyleProperty: IDomStyleProperty
	{
		public string Property { get; }
		public string Value { get; }

		public DomStyleProperty(string property, string value)
		{
			Property = property;
			Value = value;
		}

		public override string ToString()
		{
			return Property + ": " + Value;
		}
	}
}