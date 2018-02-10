namespace FluffySpoon.Automation.Web.Dom
{
	public class DomAttribute: IDomAttribute
	{
		public string Name { get; }
		public string Value { get; }

		public DomAttribute(string name, string value)
		{
			Name = name;
			Value = value;
		}

		public override string ToString()
		{
			return Name + ": " + Value;
		}
	}
}