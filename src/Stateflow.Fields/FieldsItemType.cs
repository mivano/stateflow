using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Stateflow.Fields
{



	/// <summary>
	/// Describes a type containing fields.
	/// </summary>
	public class FieldsItemType<TIdentifier>: IFieldsItemType<TIdentifier>
	{

		IFieldsItemStore<TIdentifier> _store;
		FieldDefinitionCollection<TIdentifier> _fieldDefinitions;
		private Object _lock = new object();

		public FieldsItemType (IFieldsItemStore<TIdentifier> store)
		{
			_store = store;
			
		}

		#region IFieldsItemType implementation

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
			get {
				throw new NotImplementedException ();
			}
		}

		public string Description {
			get {
				throw new NotImplementedException ();
			}
		}

		public IFieldsItemStore<TIdentifier> Store {
			get {
				return _store;
			}
		}

		#endregion

		#region IIdentifiableBy implementation

		public TIdentifier Id {
			get {
				throw new NotImplementedException ();
			}
			set {
				throw new NotImplementedException ();
			}
		}

		#endregion
	}
	
}
