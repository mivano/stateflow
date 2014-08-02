using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Stateflow.Data
{

	public interface IRepository<TKey, TItem>
		where TItem : class, IIdentifiableBy<TKey>
	{
		IQueryable<TItem> GetAll();
		TItem Get(TKey key);
		TKey Set(TItem item);
		TItem Remove(TKey key);
	}
}
