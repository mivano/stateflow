using System;
using System.Collections;
using System.Collections.Generic;

namespace Stateflow.Fields
{
	/// <summary>
	/// A collection of fields. Fields are based on field definitions.
	/// </summary>
	public class FieldCollection<TIdentifier> : Dictionary <TIdentifier, IField<TIdentifier>>
	{

		private readonly FieldDefinitionCollection<TIdentifier> _fieldDefinitions;

		private readonly IRevision<TIdentifier> _revision;

		public FieldCollection (IRevision<TIdentifier> revision, FieldDefinitionCollection<TIdentifier> fieldDefinitions)
		{
			_revision = revision;
			_fieldDefinitions = fieldDefinitions;
		}

		public void Add(IField<TIdentifier> field){
			this.Add (field.Id, field);
		}

		public IField<TIdentifier> this [string name] {
			get {
				if (name == null) {
					throw new ArgumentNullException ("name");
				}
				var definition = _fieldDefinitions [name];
				return this.GetById (definition.Id);
			}
		}



		public IField<TIdentifier> GetById(TIdentifier id)
		{
		
			IField<TIdentifier> field;
			if (!this.TryGetValue (id, out field)) {
					
				IFieldDefinition<TIdentifier> fd;
				if (_fieldDefinitions.TryGetValue (id, out fd)) {
						
					field = new Field<TIdentifier> (_revision, fd);
					this.Add (field);
						
				}
					
			}
			return field;

		}
	}

}
