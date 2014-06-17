using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Stateflow.Fields
{

	public interface IFieldsItemStore<TIdentifier>{

		void SaveFieldsItems(IEnumerable<FieldsItem<TIdentifier>> fieldsItems);

	}

	public class InMemoryFieldsItemStore<TIdentifier>: IFieldsItemStore<TIdentifier>
	{

		private Dictionary<TIdentifier,  FieldsItem<TIdentifier>> _items;

		public InMemoryFieldsItemStore ()
		{
			_items = new Dictionary<TIdentifier, FieldsItem<TIdentifier>> ();
		}

		#region IFieldsItemStore implementation


		public void SaveFieldsItems(IEnumerable<FieldsItem<TIdentifier>> fieldsItems)
		{
			foreach (var item in fieldsItems) {
				_items [item.Id] = item;
			}
		}

		#endregion


	}
}
