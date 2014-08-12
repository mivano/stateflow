using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Stateflow.Data
{

	public interface IItemRepository<TIdentity>
	{
		Task<ItemIdentifier<TIdentity>> SaveAsync(TIdentity tIdentity, int revision, TIdentity templateId, List<FieldValue<TIdentity>> fields);
	}

}
