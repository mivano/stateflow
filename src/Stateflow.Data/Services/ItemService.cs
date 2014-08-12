using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Stateflow.Data
{

	public class ItemService<TIdentity>{

		IItemRepository<TIdentity> itemRepository;

		public ItemService (IItemRepository<TIdentity> itemRepository)
		{
			if (itemRepository == null)
				throw new ArgumentNullException ("itemRepository");

			this.itemRepository = itemRepository;
			
		}

		public virtual Task<Item<TIdentity>> GetByIdAsync(ItemIdentifier<TIdentity> id){
			if (id == null)
				throw new ArgumentNullException ("id");

			return null;

		}

		public virtual Task<QueryResult<TIdentity>> QueryAsync(){

			return null;
		}

		public virtual async Task<Item<TIdentity>> CreateAsync(TIdentity templateId, IEnumerable<FieldValue<TIdentity>> fieldValues){

			// Create the new item
			var item = new Item<TIdentity> ();

			// Set the initial ID
			var identifier = new ItemIdentifier<TIdentity>{ TemplateId = templateId, Revision = 0 };

			// Set the fields
			var fields = new List<FieldValue<TIdentity>> ();

			// Get the default field definitions from the template
			// TODO

			// Merge the field values passed in
			foreach (var field in fieldValues) {
				var f = fields.FirstOrDefault (a => a.FieldDefinitionId.Equals(field.FieldDefinitionId));
				if (f == null)
					fields.Add (field);
				else {
					fields.Remove (f);
					fields.Add (field);
				}
			}

			// Validate fields
			// TODO

			// Store the data
			identifier = await itemRepository.SaveAsync (default(TIdentity), 0, templateId, fields);

			item.Identifier = identifier;
			item.FieldValues = fields;

			return item;
		}

		public virtual  Task<Item<TIdentity>> UpdateAsync(ItemIdentifier<TIdentity> itemId, IEnumerable<FieldValue<TIdentity>> fieldValues){

			// Store the data
			//var identifier = await itemRepository.SaveAsync (itemId.Id, itemId.Revision, itemId.TemplateId, fields);

			//item.Identifier = identifier;
			//item.FieldValues = fields;

			//return item;
			return null;
		}

		public virtual Task<Item<TIdentity>> DeleteAsync(ItemIdentifier<TIdentity> itemId){

			return null;
		}

	}

}
