using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Collections.Concurrent;

namespace Stateflow.Fields.DataStores
{

	public class InMemoryDataStore<TIdentifier>: IDataStore<TIdentifier>
	{
		private object _lock = new object();
		private IRepository<TIdentifier, ITemplate<TIdentifier>> _template;
		private IRepository<TIdentifier, IFieldsItem<TIdentifier>> _items;


		#region IDataStore implementation

		public IRepository<TIdentifier, ITemplate<TIdentifier>> Templates {
			get {
				if (_template == null) {
					lock (_lock) {
						if (_template == null)
							_template = new InMemoryRepository<TIdentifier, ITemplate<TIdentifier>> ();
					}
				}
				return _template;
			}
		}

		public IRepository<TIdentifier, IFieldsItem<TIdentifier>> Items {
			get {

				if (_items == null) {
					lock (_lock) {
						if (_items == null)
							_items = new InMemoryRepository<TIdentifier, IFieldsItem<TIdentifier>> ();
					}
				}
				return _items;
			}
		}

		#endregion


	

	}

}
