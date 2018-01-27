using System.Collections.Generic;
using System.Linq;

namespace FluffySpoon.Automation.Web.Dom
{
	public class DomAttributes : IDomAttributes
	{
		private readonly ISet<IDomAttribute> _attributes;

		public DomAttributes()
		{
			_attributes = new HashSet<IDomAttribute>();
		}

		public string this[string name] {
			get {
				return _attributes
					.FirstOrDefault(x => x.Name == name)
					.Value;
			}
		}

		public void Remove(string name)
		{
			Remove(_attributes.FirstOrDefault(x => x.Name == name));
		}

		public void Remove(IDomAttribute domAttribute)
		{
			_attributes.Remove(domAttribute);
		}

		public void Add(IDomAttribute domAttribute) {
			_attributes.Add(domAttribute);
		}

		public void Add(string name, string value) {
			Add(new DomAttribute(name, value));
		}
    }
}
