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

		public virtual Task<Item<TIdentity>> CreateAsync(TIdentity templateId, IEnumerable<FieldValue<TIdentity>> fieldValues){

			return null;
		}

		public virtual Task<Item<TIdentity>> UpdateAsync(ItemIdentifier<TIdentity> itemId, IEnumerable<FieldValue<TIdentity>> fieldValues){

			return null;
		}

		public virtual Task<Item<TIdentity>> DeleteAsync(ItemIdentifier<TIdentity> itemId){

			return null;
		}

	}

}
