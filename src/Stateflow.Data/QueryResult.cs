using System;
using System.Collections.Generic;
using System.Linq;

namespace Stateflow.Data
{

	public class QueryResult<TIdentity>{

		public IEnumerable<Item<TIdentity>> Items {
			get;
			set;
		}

		public long TotalItems {
			get;
			set;
		}

		public int Page {
			get;
			set;
		}

		public int PageSize {
			get;
			set;
		}

	}
}
