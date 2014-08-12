using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Collections.Concurrent;
using System.Threading.Tasks;

namespace Stateflow.Data.Store.InMemory
{

	public class InMemoryItemsRepository<TIdentity> : IItemRepository<TIdentity>{

		private readonly ConcurrentDictionary<ItemIdentifier<TIdentity>, List<FieldValue<TIdentity>>> _store = new ConcurrentDictionary<ItemIdentifier<TIdentity>, List<FieldValue<TIdentity>>>();


		#region IItemRepository implementation

		public Task<ItemIdentifier<TIdentity>> SaveAsync(TIdentity tIdentity, int revision, TIdentity templateId, List<FieldValue<TIdentity>> fields)
		{
			// Up revision number
			revision++;

			var identifier = new ItemIdentifier<TIdentity> {
				Revision = revision,
				TemplateId = templateId,
				Id = tIdentity
			};

			if (default(TIdentity).Equals (tIdentity)) {
				// Create new

			}

			_store.AddOrUpdate(identifier, o => fields, (o, k) => fields);

			return Task.FromResult(identifier);
		}

		#endregion


	}
}
