using System;
using System.Collections;
using System.Collections.Generic;

namespace Stateflow.Fields
{

	public class FieldDefinition<TIdentifier> : IFieldDefinition<TIdentifier>
    {
        private IDictionary<string, object> _metaData;
        private readonly object _lock = new object();

        public FieldDefinition()
        {
            // Default value
            IsEditable = true;
        }

		public FieldDefinition (FieldType fieldType, TIdentifier id, string name): this()
		{
			Id = id;
			FieldType = fieldType;
			Name = name;
		}

        #region IFieldDefinition implementation

		public virtual FieldType FieldType
        {
            get;
            set;
        }

		/// <summary>
		/// Determines the order of the field.
		/// </summary>
		/// <value>The sequence.</value>
		public virtual int Sequence { get; set;}

		public virtual IEnumerable AllowedValues { get; set; }

		public virtual bool IsComputed { get; set; }

		public virtual bool IsEditable { get; set; }

		public virtual object DefaultValue { get; set; }

		public virtual string Name { get; set; }

		public virtual string ReferenceName { get; set; }

		public virtual string Description { get; set; }

		public virtual Type SystemType { get; set; }

		public virtual IFieldOptions FieldOptions { get; set; }

		public virtual IEnumerable<IFieldValidator<TIdentifier>> Validators { get; set; }

		public virtual IDictionary<string, object> MetaData
        {
            get
            {

                if (_metaData == null)
                {
                    lock (_lock)
                    {
                        if (_metaData == null)
                            _metaData = new Dictionary<string, object>();
                    }
                }
                return _metaData;
            }
            set
            {
                _metaData = value;
            }
        }

        #endregion

        #region IIdentifiableBy implementation
		public virtual TIdentifier Id { get; set; }

        #endregion

    }

}
