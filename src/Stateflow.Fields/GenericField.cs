using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Stateflow.Fields
{
	/// <summary>
	/// A generic implementation of a field.
	/// </summary>
	public class GenericField<TIdentifier> : IField<TIdentifier>, IFieldActions
	{
		public GenericField ()
		{
		}

		#region IField implementation

		public object Value { get; set; }

		public string Name{ get; set; }

		public string Description{ get; set; }

		public FieldType FieldType { get; set; }

		public IFieldOptions FieldOptions { get; set; }

		public int Sequence { get; set; }

		public IEnumerable<IFieldValidator<TIdentifier>> Validators { get; set; }

		#endregion

		#region ICorrelateBy implementation

		public TIdentifier Id { get; set; }

		#endregion

		public bool IsValid(){
			if (Validators == null || Validators.Any () == false)
				return true;

			return Validators.All (a => a.IsValid (this));
		}
	}

}
