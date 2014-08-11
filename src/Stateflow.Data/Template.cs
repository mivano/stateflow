using System;
using System.Collections.Generic;
using System.Linq;

namespace Stateflow.Data
{

	/// <summary>
	/// Describes the standard available fields for a specific template.
	/// </summary>
	public class Template<TIdentity>: IIdentifiableBy<TIdentity>
	{
		public Template ()
		{
			FieldDefinitions = new List<TemplateFieldDefinition<TIdentity>> ();
		}

		#region IIdentifiableBy implementation

		public TIdentity Id {
			get;
			set;
		}

		#endregion

		public string Name {
			get;
			set;
		}

		/// <summary>
		/// Defines the available fields for this template.
		/// </summary>
		/// <value>The field definitions.</value>
		public IList<TemplateFieldDefinition<TIdentity>> FieldDefinitions {
			get;
			set;
		}
	}

}
