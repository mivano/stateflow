using System;
using System.Collections.Generic;
using System.Linq;

namespace Stateflow.Data
{
	/// <summary>
	/// A wrapper to access the different services to interact with the data.
	/// </summary>
	public class Store<TIdentity>{

		private readonly ItemService<TIdentity> itemService;

		private readonly TemplateService<TIdentity> templateService;

		private readonly FieldDefinitionService<TIdentity> fieldDefinitionService;

		public Store (ItemService<TIdentity> itemService, 
			TemplateService<TIdentity> templateService,
			FieldDefinitionService<TIdentity> fieldDefinitionService
		)
		{
			this.fieldDefinitionService = fieldDefinitionService;
			this.templateService = templateService;
			this.itemService = itemService;
			
		}

		public TemplateService<TIdentity> Templates{ get { return this.templateService; } }

		public FieldDefinitionService<TIdentity> FieldDefinitions{ get { return this.fieldDefinitionService; } }

		public ItemService<TIdentity> Items{ get { return this.itemService; } }

	}

}
