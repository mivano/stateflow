using NUnit.Framework;
using Moq;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Stateflow.Data.Tests
{

	[TestFixture]
	public class FieldDefinitionServiceTests{

		[Test]
		public void StoreFieldDefinition(){

			var f = new FieldDefinition<int> ();
			f.Id = 1;
			f.Name = "test";
			f.FieldType = FieldType.Boolean;

			var r = new Mock<IFieldDefinitionRepository<int>> ();
			r.Setup (a => a.SetAsync (It.IsAny<FieldDefinition<int>>())).Returns (Task.FromResult(1));

			var fs = new FieldDefinitionService<int> (r.Object);

			var id = fs.SaveAsync (f).Result;

			Assert.AreEqual (id, f.Id);

		}

	}   
}
