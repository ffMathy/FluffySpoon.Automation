namespace FluffySpoon.Automation.Web.Dom
{
	public interface IDomAttributes
	{
		string this[string name] { get; }

		void Add(IDomAttribute domAttribute);
		void Add(string name, string value);
		void Remove(IDomAttribute domAttribute);
		void Remove(string name);
	}
}