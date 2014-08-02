using System;
using System.Collections.Generic;
using System.Linq;

namespace Stateflow.Data
{
	/// <summary>
	/// Defines a single field.
	/// </summary>
	public class FieldDefinition<TIdentity>: IIdentifiableBy<TIdentity>
	{
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
		/// A key value collection of additional metadata. Can be used to handle additional validations or UI directives.
		/// </summary>
		/// <value>The meta data.</value>
		public IDictionary<string, object> MetaData{ get; set;}

		/// <summary>
		/// The field type to use.
		/// </summary>
		/// <value>The type of the field.</value>
		public FieldType FieldType {
			get;
			set;
		}

		/// <summary>
		/// When the field type is a reference, this indicates where it references to.
		/// </summary>
		/// <value>The reference item.</value>
		public TIdentity ReferenceItem {
			get;
			set;
		}
	}

















}

