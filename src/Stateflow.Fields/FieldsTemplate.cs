using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Stateflow.Fields
{



	/// <summary>
	/// Describes a template containing fields.
	/// </summary>
	public class FieldsTemplate<TIdentifier>: IFieldsTemplate<TIdentifier>
	{

		IFieldsItemStore<TIdentifier> _store;
		FieldDefinitionCollection<TIdentifier> _fieldDefinitions;
		private Object _lock = new object();

		public FieldsTemplate (IFieldsItemStore<TIdentifier> store)
		{
			_store = store;
			
		}

		public FieldsTemplate (IFieldsItemStore<TIdentifier> store, string name)
		{
			_store = store;
			Name = name;
		}

		#region IFieldsTemplate implementation

		public virtual IFieldsItem<TIdentifier> CreateNew(){
			return new FieldsItem<TIdentifier> (this);
		}

		public FieldDefinitionCollection<TIdentifier> FieldDefinitions {
			get {
				if (_fieldDefinitions == null) {
					lock (_lock) {
						if (_fieldDefinitions == null) {
							_fieldDefinitions = new FieldDefinitionCollection<TIdentifier> (_store, this);
						}
					}
				}
				return _fieldDefinitions;
			}
		}

		public string Name {
			get ;
			set;
		}

		public string Description {
			get;
			set;
		}

		public Version Version{ get; set; }

		public IFieldsItemStore<TIdentifier> Store {
			get {
				return _store;
			}
		}

		#endregion

		#region IIdentifiableBy implementation

		public TIdentifier Id{ get; set;}

		#endregion
	}
	
}
