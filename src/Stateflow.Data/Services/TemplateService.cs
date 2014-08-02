using System;
using System.Collections.Generic;
using System.Linq;

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

		public virtual IEnumerable<Template<TIdentity>> GetTemplates(){
			return templateRepository.GetAll ().ToList ();
		}

		public virtual Template<TIdentity> GetTemplateyId(TIdentity id){

			if (id == null)
				throw new ArgumentNullException ("id");

			return templateRepository.Get (id);
		}

		public virtual Template<TIdentity> GetTemplateByName(string name){

			if (string.IsNullOrWhiteSpace(name))
				throw new ArgumentNullException ("name");

			return templateRepository.GetAll ().FirstOrDefault (a => a.Name.Equals (name, StringComparison.InvariantCultureIgnoreCase));
		}

		public virtual TIdentity SaveTemplate(Template<TIdentity> template){
			var identity = templateRepository.Set (template);

			return identity;
		}

		public virtual Template<TIdentity> DeleteTemplate( TIdentity id){
			return templateRepository.Remove (id);
		}

	}

}
