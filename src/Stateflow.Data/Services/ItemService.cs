using System;
using System.Collections.Generic;
using System.Linq;

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

		public virtual Item<TIdentity> GetById(ItemIdentifier<TIdentity> id){
			if (id == null)
				throw new ArgumentNullException ("id");

			return null;

		}

		public virtual QueryResult<TIdentity> Query(){

			return null;
		}

		public virtual Item<TIdentity> Create(TIdentity templateId, IEnumerable<FieldValue<TIdentity>> fieldValues){

			return null;
		}

		public virtual Item<TIdentity> Update(ItemIdentifier<TIdentity> itemId, IEnumerable<FieldValue<TIdentity>> fieldValues){

			return null;
		}

		public virtual Item<TIdentity> Delete(ItemIdentifier<TIdentity> itemId){

			return null;
		}

	}

}
