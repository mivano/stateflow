using NUnit.Framework;
using System;
using System.Linq;
using System.Collections.Generic;

namespace Stateflow.Fields.Tests
{
	[TestFixture ()]
	public class CreationTest
	{
		[Test ()]
		public void CanCreateCollection ()
		{
			var c = new FieldCollection<int> ();

			Assert.IsNotNull (c);

			c.Add (new GenericField<int>{ });

			Assert.IsTrue (c.Count == 1);
		}

		[Test()]
		public void CollectionCanBeAField()
		{
			var c = new FieldCollection<int> ();

			var n = new FieldCollection<int> ();

			c.Add (n);

			Assert.IsNotNull(c);
			Assert.IsTrue (c.Count () == 1);
			Assert.AreSame (c.First ().Value, n);
		}

		[Test()]
		public void CollectionCanBeAddedUsingMethod(){
			var c = new FieldCollection<int> ();

			var sub = c.AddCollection ("collection");

			Assert.IsNotNull (sub);
			Assert.IsTrue (c.Count == 1);
			Assert.AreEqual (c.First ().Value, sub);
			Assert.AreEqual (sub.Name, "collection");
			Assert.AreEqual (sub.FieldType, FieldType.FieldCollection);

		}
	}

}

