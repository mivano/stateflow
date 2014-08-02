using System;
using System.Collections.Generic;
using System.Linq;

namespace Stateflow.Data
{

	public interface IFieldDefinitionRepository<TIdentity>: IRepository<TIdentity, FieldDefinition<TIdentity>>
	{

	}

}
