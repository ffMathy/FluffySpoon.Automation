using System.Collections.Generic;
using System.Linq;

namespace FluffySpoon.Automation.Web.Dom
{
	public class DomStyle : IDomStyle
	{
		private readonly ICollection<IDomStyleProperty> _styleProperties;

		public DomStyle(ICollection<IDomStyleProperty> styleProperties)
		{
			_styleProperties = styleProperties;
		}

		public string this[string property] {
			get {
				return _styleProperties
					.FirstOrDefault(x => x.Property == property)
					.Value;
			}
		}
    }
}
