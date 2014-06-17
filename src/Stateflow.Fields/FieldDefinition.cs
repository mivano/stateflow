using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Stateflow.Fields
{
	public class FieldDefinition<TIdentifier>: IFieldDefinition<TIdentifier>
	{
		public FieldDefinition ()
		{
			// Default value
			IsEditable = true;
		}

		#region IFieldDefinition implementation

		public FieldType FieldType { get; set;
		}

		public IEnumerable AllowedValues {
			get {
				throw new NotImplementedException ();
			}
			set {
				throw new NotImplementedException ();
			}
		}

		public bool IsComputed { get; set;		}

		public bool IsEditable{ get; set;		}

		public object DefaultValue { get; set;		}

		public string Name { get; set;		}

		public string ReferenceName { get; set;		}

		public string Description { get; set;		}

		public Type SystemType { get; set;		}

		public IFieldOptions FieldOptions { get; set;		}

		public IEnumerable<IFieldValidator<TIdentifier>> Validators { get; set;		}

		#endregion

		#region IIdentifiableBy implementation

		public TIdentifier Id { get; set;		}

		#endregion

	}

}
