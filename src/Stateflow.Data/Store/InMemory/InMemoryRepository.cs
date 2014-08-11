using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Collections.Concurrent;
using System.Threading.Tasks;

namespace Stateflow.Data.Store.InMemory
{



	public class InMemoryRepository<TKey, TItem> : IRepository<TKey, TItem>
		where TItem : class, IIdentifiableBy<TKey>
	{
		private readonly ConcurrentDictionary<TKey, TItem> _store = new ConcurrentDictionary<TKey, TItem>();

		#region IRepository implementation

		public System.Threading.Tasks.Task<IQueryable<TItem>> GetAllAsync()
		{
			return Task.FromResult(GetAll());
		}

		public System.Threading.Tasks.Task<TItem> GetAsync(TKey id)
		{
			return Task.FromResult(Get(id));
		}

		public System.Threading.Tasks.Task<TKey> SetAsync(TItem item)
		{
			return Task.FromResult(Set(item));
		}

		public System.Threading.Tasks.Task<TItem> RemoveAsync(TKey key)
		{
			return Task.FromResult(Remove(key));
		}

		#endregion

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
