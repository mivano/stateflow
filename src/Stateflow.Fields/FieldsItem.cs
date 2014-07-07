using System;
using Stateflow.Fields.DataStores;

namespace Stateflow.Fields
{

    /// <summary>
    /// A fields item contains field definitions and their values and is based on a Template
    /// </summary>
    public class FieldsItem<TIdentifier> : IFieldsItem<TIdentifier>
    {

        private readonly ITemplate<TIdentifier> _template;
        private FieldCollection<TIdentifier> _fields;
        private RevisionCollection<TIdentifier> _revisions;
        private FieldData<TIdentifier> _fieldData;
        private IDataStore<TIdentifier> _store;

		/// <summary>
		/// Initializes a new instance of the <see cref="Stateflow.Fields.FieldsItem`1"/> class based on a template.
		/// </summary>
		/// <param name="template">Template.</param>
        public FieldsItem(ITemplate<TIdentifier> template)
        {
            if (template == null)
                throw new ArgumentNullException("template");

            _template = template;
            Id = default(TIdentifier);
            _fieldData = new FieldData<TIdentifier>(this);
            _store = template.Store;
        }

		/// <summary>
		/// Initializes a new instance of the <see cref="Stateflow.Fields.FieldsItem`1"/> class based on a template and by passing in field data.
		/// </summary>
		/// <param name="template">Template.</param>
		/// <param name="fieldData">Field data.</param>
		/// <param name="id">Identifier.</param>
		public FieldsItem(ITemplate<TIdentifier> template, FieldData<TIdentifier> fieldData, TIdentifier id)
		{
			if (fieldData == null)
				throw new ArgumentNullException ("fieldData");

			if (template == null)
				throw new ArgumentNullException("template");

			_template = template;
			Id = id;
			_store = template.Store;
			_fieldData = fieldData;
		}

        public TIdentifier Id { get; set; }

        #region IRevision implementation

        public virtual object GetCurrentFieldValue(IFieldDefinition<TIdentifier> fieldDefinition)
        {
            return GetFieldValue(fieldDefinition.Id, -1);
        }

        public virtual object GetOriginalFieldValue(IFieldDefinition<TIdentifier> fieldDefinition)
        {
            return GetFieldValue(fieldDefinition.Id, -2);
        }

        public virtual void SetFieldValue(IFieldDefinition<TIdentifier> fieldDefinition, object value)
        {
            SetFieldValueInternal(fieldDefinition, value);
        }

        private void SetFieldValueInternal(IFieldDefinition<TIdentifier> fieldDefinition, object value)
        {

            // get current value
            var currentValue = _fieldData.GetFieldValue(fieldDefinition.Id, -1);

            // Only change when actually different
            if (!object.Equals(currentValue, value))
            {
                if (!fieldDefinition.IsEditable)
                {
                    throw new ValidationException(string.Format("The field '{0}' is readonly.", fieldDefinition.Name));
                }

                _fieldData.SetFieldValue(fieldDefinition.Id, value);
            }


        }

		public virtual object GetFieldValue(TIdentifier id, int revision)
        {
            return _fieldData.GetFieldValue(id, revision);
        }


		public virtual ITemplate<TIdentifier> Template
        {
            get
            {
                return _template;
            }
        }

		public virtual bool IsReadOnly
        {
            get
            {
                return false;
            }
        }

		public virtual bool IsDraft
        {
            get
            {
				return false;
            }
        }

		public virtual int Number
        {
            get
            {
                return -1;
            }
        }

        /// <summary>
        /// Sets or gets the value of a field directly using the identifier of the field.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
		public virtual object this[TIdentifier id]
        {
            get
            {
                if (id.Equals(default(TIdentifier)))
                {
                    throw new ArgumentNullException("id");
                }
                return Fields[id].Value;
            }
            set
            {
                if (id.Equals(default(TIdentifier)))
                {
                    throw new ArgumentNullException("id");
                }
                Fields[id].Value = value;
            }
        }

        /// <summary>
        /// Gets or sets the value of a field using the name of the field.
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
		public virtual object this[string name]
        {
            get
            {
                if (string.IsNullOrEmpty(name))
                {
                    throw new ArgumentNullException("name");
                }
                return Fields[name].Value;
            }
            set
            {
                if (string.IsNullOrEmpty(name))
                {
                    throw new ArgumentNullException("name");
                }
                Fields[name].Value = value;
            }
        }

        // Save the item, this will create a new revision.
		public virtual void Save()
        {
            if (!IsDirty) return;

            // Create a new revision
            _fieldData.CreateNewRevision();

            // Store the data, this should create a new identifier
            _store.SaveItem(this);

            // Refresh the revision list
            Revisions.PrepareRevisions();
        }

      

		public virtual bool Validate()
        {
            throw new NotImplementedException();
        }


        #endregion

		public virtual FieldCollection<TIdentifier> Fields
        {

            get
            {
                if (_fields == null)
                {
                    _fields = new FieldCollection<TIdentifier>(this, Template.FieldDefinitions);
                }
                return _fields;
            }

        }

		public virtual FieldData<TIdentifier> FieldData
        {
            get
            {
                return _fieldData;
            }
        }



		public virtual bool IsDirty
        {
            get
            {
                return _fieldData.IsDirty();
            }
        }

		public virtual  bool IsNew
        {
            get
            {
                return Id.Equals(default(TIdentifier));
            }
        }

		public virtual int RevisionNumber
        {
            get
            {
                return _fieldData.Versions;
            }
        }

		public virtual RevisionCollection<TIdentifier> Revisions
        {
            get
            {
                if (_revisions == null)
                {
                    _revisions = new RevisionCollection<TIdentifier>(this);
                }
                return _revisions;
            }
        }
    }


}
