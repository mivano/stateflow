using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Collections.Concurrent;

namespace Stateflow.Fields.DataStores
{

	public class InMemoryRepository<TKey, TItem> : IRepository<TKey, TItem>
		where TItem : class, IIdentifiableBy<TKey>
	{
		private readonly ConcurrentDictionary<TKey, TItem> _store = new ConcurrentDictionary<TKey, TItem>();

		public virtual IQueryable<TItem> GetAll()
		{
			return _store.Values.AsQueryable();
		}

		public virtual TItem Get(TKey key)
		{
			TItem value;
			if (_store.TryGetValue (key, out value))
				return value;
			return default(TItem);
		}

		public virtual TKey Set(TItem state)
		{
			_store.AddOrUpdate(state.Id, o => state, (o, k) => state);

			return state.Id;
		}

		public virtual TItem Remove(TKey key)
		{
			TItem value;
			_store.TryRemove(key, out value);
			return value;
		}

	}
}
