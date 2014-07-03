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

        public FieldsItem(ITemplate<TIdentifier> template)
        {
            if (template == null)
                throw new ArgumentNullException("template");

            _template = template;
            Id = default(TIdentifier);
            _fieldData = new FieldData<TIdentifier>(this);
            _store = template.Store;
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

        public object GetFieldValue(TIdentifier id, int revision)
        {
            return _fieldData.GetFieldValue(id, revision);
        }


        public ITemplate<TIdentifier> Template
        {
            get
            {
                return _template;
            }
        }

        public bool IsReadOnly
        {
            get
            {
                return false;
            }
        }

        public bool IsDraft
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        public int Number
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
        public object this[TIdentifier id]
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
        public object this[string name]
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
        public void Save()
        {
            if (!IsDirty) return;

            // Create a new revision
            _fieldData.CreateNewRevision();

            // Store the data, this should create a new identifier
            _store.SaveItem(this);

            // Refresh the revision list
            Revisions.PrepareRevisions();
        }

      

        public bool Validate()
        {
            throw new NotImplementedException();
        }


        #endregion

        public FieldCollection<TIdentifier> Fields
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

        public FieldData<TIdentifier> FieldData
        {
            get
            {
                return _fieldData;
            }
        }



        public bool IsDirty
        {
            get
            {
                return _fieldData.IsDirty();
            }
        }

        public bool IsNew
        {
            get
            {
                return Id.Equals(default(TIdentifier));
            }
        }

        public int RevisionNumber
        {
            get
            {
                return _fieldData.Versions;
            }
        }

        public RevisionCollection<TIdentifier> Revisions
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
