using System;
using System.Collections.Generic;
using System.Linq;

namespace Stateflow.Data
{

	/// <summary>
	/// Describes the field needed for a template.
	/// </summary>
	public class TemplateFieldDefinition<TIdentity>{

		public TemplateFieldDefinition ()
		{
			
		}

		public TemplateFieldDefinition (TIdentity fieldDefinition, bool isRequired = false)
		{
			FieldDefinition = fieldDefinition;
			IsRequired = isRequired;
		}

		public TIdentity FieldDefinition {
			get;
			set;
		}

		public bool IsRequired {
			get;
			set;
		}
	}

}
