using NUnit.Framework;
using System;
using System.Linq;
using System.Collections.Generic;

namespace Stateflow.Fields.Tests
{
	[TestFixture]
	public class CreationTest
	{
	
		[Test]
		public void CanCreateFieldDefinition(){
			var fd = new FieldDefinition<int> ();
			fd.Id = 1;
			fd.Name = "test";

			Assert.AreEqual (fd.Id, 1);
			Assert.AreEqual (fd.Name, "test");
		}

		[Test]
		public void CanAddFieldDefinitions(){
			var fd = new FieldDefinition<int> ();
			fd.Id = 1;

			var fdc = new FieldDefinitionCollection<int> ();
			fdc.Add (fd);

			Assert.IsTrue (fdc.Count == 1);
			Assert.AreSame (fdc.First ().Value, fd);
			Assert.AreSame (fdc [1], fd);
		}

		[Test]
		public void CanCreateAFieldsItemAndAssingValue(){

			var fd = new FieldDefinition<int> ();
			fd.Id = 1;

			var fit = new FieldsItemType<int> (new InMemoryFieldsItemStore<int>());
			fit.FieldDefinitions.Add (fd);

			var fi = new FieldsItem<int> (fit);

			var f = new Field<int> (fi,fd);

			Assert.AreEqual (f.Id , fd.Id);

			Assert.IsFalse (fi.IsDirty);

			f.Value = "test";

			Assert.AreEqual (f.Value, "test");

			Assert.AreEqual (f.OriginalValue, null);

			Assert.AreEqual (fi.Fields [f.Id].Value, "test");

			Assert.IsTrue (fi.IsDirty);

			fi.Save ();

			var revisions = fi.Revisions;

			Assert.IsNotNull (revisions);


		}
	}

}

