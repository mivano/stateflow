using System;
using System.Collections.Generic;
using System.Linq;

namespace Stateflow.Data
{

	public class FieldDefinitionService<TIdentity>
	{

		readonly IFieldDefinitionRepository<TIdentity> fieldDefinitionRepository;

		public FieldDefinitionService (IFieldDefinitionRepository<TIdentity> fieldDefinitionRepository)
		{
			if (fieldDefinitionRepository == null)
				throw new ArgumentNullException ("fieldDefinitionRepository");

			this.fieldDefinitionRepository = fieldDefinitionRepository;
		}

		public virtual IEnumerable<FieldDefinition<TIdentity>> GetFieldDefitions(){
			return fieldDefinitionRepository.GetAll ().ToList ();
		}

		public virtual FieldDefinition<TIdentity> GetFieldDefinitionById(TIdentity id){

			if (id == null)
				throw new ArgumentNullException ("id");

			return fieldDefinitionRepository.Get (id);
		}

		public virtual FieldDefinition<TIdentity> GetFieldDefinitionByName(string name){

			if (string.IsNullOrWhiteSpace(name))
				throw new ArgumentNullException ("name");

			return fieldDefinitionRepository.GetAll ().FirstOrDefault (a => a.Name.Equals (name, StringComparison.InvariantCultureIgnoreCase));
		}

	}

}
