using System;
using System.Collections.Generic;
using System.Linq;

namespace Stateflow.Data
{

	/// <summary>
	/// An item that contains data fields.
	/// </summary>
	public class Item<TIdentity>
	{
		List<FieldValue<TIdentity>> fieldValues;

		public ItemIdentifier<TIdentity> Id {
			get;
			set;
		}

		public TIdentity TemplateId {
			get;
			set;
		}

		public string Name {
			get{
				var nameField = GetFieldByName ("ItemName");
				if (nameField == null)
					return null;
				else
					return nameField.Data.DisplayValue;
			}
			set{
				var nameField = GetFieldByName ("ItemName");
				if (nameField == null) {
					nameField = new FieldValue<TIdentity> {
						FieldDefinitionName = "ItemName",
						Data = new FieldData<TIdentity>{
						 
						}
					};
					FieldValues.Add (nameField);
				}

				nameField.Data.DisplayValue = value;
			    nameField.Data.Value = value;

			}
		}


		/// <summary>
		/// Gets or sets the field values.
		/// </summary>
		/// <value>The field values.</value>
		public List<FieldValue<TIdentity>> FieldValues {
			get {
				if (fieldValues == null)
					fieldValues = new List<FieldValue<TIdentity>> ();

				return fieldValues;
			}
			set {
				fieldValues = value;
			}
		}

		/// <summary>
		/// Gets a value indicating whether this instance is deleted.
		/// </summary>
		/// <value><c>true</c> if this instance is deleted; otherwise, <c>false</c>.</value>
		public bool IsDeleted {
			get{
				var nameField = GetFieldByName ("ItemDeleted");
				if (nameField == null)
					return false;
				else
					return (bool)nameField.Data.Value;
			}
		}

		protected virtual FieldValue<TIdentity> GetFieldByName(string name){
			if (FieldValues==null)
				return null;

			var field = FieldValues.FirstOrDefault(a=>a.FieldDefinitionName.Equals(name, StringComparison.InvariantCultureIgnoreCase));

			return field;
		}
	}

}
