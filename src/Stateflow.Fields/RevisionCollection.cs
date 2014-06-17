using System;
using System.Collections;
using System.Collections.Generic;

namespace Stateflow.Fields
{

	public class RevisionCollection<TIdentifier> : Dictionary<TIdentifier, IRevision<TIdentifier>>
	{

		private readonly IFieldsItem<TIdentifier> item;

		public RevisionCollection (IFieldsItem<TIdentifier> item)
		{
			this.item = item;
			if (item == null)
				throw new ArgumentNullException ("item");
			
		}

	}


}
