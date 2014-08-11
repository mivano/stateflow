using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Collections.Concurrent;
using System.Threading.Tasks;

namespace Stateflow.Data.Store.InMemory
{

	public class InMemoryTemplateRepository<TIdentity>:  InMemoryRepository<TIdentity, Template<TIdentity>>, 
																ITemplateRepository<TIdentity>
	{

	}
	
}
