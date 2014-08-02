using NUnit.Framework;
using Moq;
using System;
using System.Collections.Generic;

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
			r.Setup (a => a.Set (It.IsAny<FieldDefinition<int>>())).Returns (1);

			var fs = new FieldDefinitionService<int> (r.Object);

			var id = fs.Save (f);

			Assert.AreEqual (id, f.Id);

		}

	}   
}
