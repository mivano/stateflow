using System;
using System.Collections.Generic;
using System.Linq;

namespace Stateflow.Data
{

	public interface ITemplateRepository<TIdentity>: IRepository<TIdentity, Template<TIdentity>>
	{

	}

}
