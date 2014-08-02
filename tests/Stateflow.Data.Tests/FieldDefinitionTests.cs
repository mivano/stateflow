using NUnit.Framework;
using Moq;
using System;
using System.Collections.Generic;

namespace Stateflow.Data.Tests
{
	public class FieldDefinitionTests
	{
		[Test ()]
		public void CanCreateFieldDefinitionAndSetProperties()
		{
			var f = new FieldDefinition<int> ();
			f.Id = 1;
			f.Name = "test";
			f.FieldType = FieldType.Boolean;

			Assert.AreEqual (f.Id, 1);
			Assert.AreEqual (f.Name, "test");
			Assert.AreEqual (f.FieldType, FieldType.Boolean);

		}

		[Test]
		public void CanSetMetaData(){
			var f = new FieldDefinition<int> ();
			f.MetaData = new Dictionary<string, object> ();
			f.MetaData.Add ("test", 1);

			Assert.IsNotNull (f);
			Assert.IsNotNull (f.MetaData);
			Assert.IsTrue (f.MetaData.Count > 0);
			Assert.AreEqual (f.MetaData ["test"], 1);
		}

	}

}

