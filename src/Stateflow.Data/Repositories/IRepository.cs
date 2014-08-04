using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Stateflow.Data
{

	public interface IRepository<TKey, TItem>
		where TItem : class, IIdentifiableBy<TKey>
	{
		Task<IQueryable<TItem>> GetAllAsync();
		Task<TItem> GetAsync(TKey id);
		Task<TKey> SetAsync(TItem item);
		Task<TItem> RemoveAsync(TKey key);
	}
}
