using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Stateflow.Data
{

	public class TemplateService<TIdentity>{

		readonly ITemplateRepository<TIdentity> templateRepository;

		public TemplateService (ITemplateRepository<TIdentity> templateRepository)
		{
			if (templateRepository == null)
				throw new ArgumentNullException ("templateRepository");

			this.templateRepository = templateRepository;
		}

		public virtual Task<IQueryable<Template<TIdentity>>> GetAllAsync(){
			return templateRepository.GetAllAsync ();
		}

		public virtual Task<Template<TIdentity>> GetByIdAsync(TIdentity id){

			if (id == null)
				throw new ArgumentNullException ("id");

			return templateRepository.GetAsync (id);
		}

		public virtual Task<Template<TIdentity>> GetByNameAsync(string name){

			if (string.IsNullOrWhiteSpace(name))
				throw new ArgumentNullException ("name");

			return Task.FromResult( GetAllAsync().Result.FirstOrDefault (a => a.Name.Equals (name, StringComparison.InvariantCultureIgnoreCase)));
		}

		public virtual Task<TIdentity> SaveAsync(Template<TIdentity> template){
			var identity = templateRepository.SetAsync (template);

			return identity;
		}

		public virtual Task<Template<TIdentity>> DeleteAsync( TIdentity id){
			return templateRepository.RemoveAsync (id);
		}

	}

}
