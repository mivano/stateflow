using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Collections.Concurrent;

namespace Stateflow.Fields.DataStores
{

	public class InMemoryDataStore<TIdentifier>: IDataStore<TIdentifier>
	{
		private readonly object _lock = new object();
		private IRepository<TIdentifier, ITemplate<TIdentifier>> _template;
		private IRepository<TIdentifier, IFieldsItem<TIdentifier>> _items;


		#region IDataStore Helpers

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

        public ITemplate<TIdentifier> LoadTemplate(TIdentifier identifier)
	    {
	        return Templates.Get(identifier);
	    }

	    public TIdentifier SaveTemplate(ITemplate<TIdentifier> template)
	    {
	        if (template==null)
            throw new ArgumentNullException("template");

	        if (template.Id.Equals(default(TIdentifier)))
	        {
	            throw new InvalidOperationException("No identifier was set. This generic inmemory repository cannot automatically assign an identifier to this object.");    
	        }

            Templates.Set(template);

	        return template.Id;
	    }

	    public TIdentifier RemoveTemplate(TIdentifier identifier)
	    {
            var item = Templates.Remove(identifier);
	        if (item == null)
	            return default(TIdentifier);

	        return item.Id;
	    }

	    public IQueryable<ITemplate<TIdentifier>> GetAllTemplates()
	    {
            return Templates.GetAll();
	    }

	    public IFieldsItem<TIdentifier> LoadItem(TIdentifier identifier)
	    {            
            return Items.Get(identifier);
	    }

	    public TIdentifier SaveItem(IFieldsItem<TIdentifier> item)
	    {
            if (item == null)
                throw new ArgumentNullException("item");

            if (item.Id.Equals(default(TIdentifier)))
            {
                throw new InvalidOperationException("No identifier was set. This generic inmemory repository cannot automatically assign an identifier to this object.");
            }

            Items.Set(item);

            return item.Id;
	    }

	    public IQueryable<IFieldsItem<TIdentifier>> GetAllItems()
	    {
	        return Items.GetAll();
	    }

	    public TIdentifier RemoveItem(TIdentifier identifier)
	    {
            var item = Items.Remove(identifier);
            if (item == null)
                return default(TIdentifier);

            return item.Id;
	    }


	

	}

}
