using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

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

		public virtual Task<IQueryable<FieldDefinition<TIdentity>>> GetAll(){
			return fieldDefinitionRepository.GetAllAsync ();
		}

		public virtual Task<FieldDefinition<TIdentity>> GetByIdAsync(TIdentity id){

			if (id == null)
				throw new ArgumentNullException ("id");

			return fieldDefinitionRepository.GetAsync (id);
		}

		public virtual Task<FieldDefinition<TIdentity>> GetByNameAsync(string name){

			if (string.IsNullOrWhiteSpace(name))
				throw new ArgumentNullException ("name");

			return Task.FromResult( GetAll().Result.FirstOrDefault (a => a.Name.Equals (name, StringComparison.InvariantCultureIgnoreCase)));
		}

		public virtual Task<TIdentity> SaveAsync(FieldDefinition<TIdentity> fieldDefinition){
			return fieldDefinitionRepository.SetAsync (fieldDefinition);

		}

		public virtual Task<FieldDefinition<TIdentity>> DeleteAsync( TIdentity id){
			return fieldDefinitionRepository.RemoveAsync(id);
		}
	}

}
