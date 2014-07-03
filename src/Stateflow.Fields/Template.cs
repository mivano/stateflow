using System;
using Stateflow.Fields.DataStores;

namespace Stateflow.Fields
{



	/// <summary>
	/// Describes a template containing fields.
	/// </summary>
	public class Template<TIdentifier>: ITemplate<TIdentifier>
	{

		IDataStore<TIdentifier> _store;
		FieldDefinitionCollection<TIdentifier> _fieldDefinitions;
		private Object _lock = new object();

        /// <summary>
        /// Creates a new Template.
        /// </summary>
        /// <param name="store"></param>
		public Template (IDataStore<TIdentifier> store)
		{
			_store = store;
			
		}

        /// <summary>
        /// Creates a new template with a specific name.
        /// </summary>
        /// <param name="store"></param>
        /// <param name="name"></param>
		public Template (IDataStore<TIdentifier> store, string name)
		{
			_store = store;
			Name = name;
		}

	
		#region ITemplate implementation

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

		public IDataStore<TIdentifier> Store {
			get {
				return _store;
			}
		}

		#endregion

		#region IIdentifiableBy implementation

		public TIdentifier Id{ get; set;}

		#endregion


        public void Save()
        {
           Id = _store.SaveTemplate(this);
        }
    }
	
}
