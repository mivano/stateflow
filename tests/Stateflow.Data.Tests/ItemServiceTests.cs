using NUnit.Framework;
using Moq;
using System;
using System.Collections.Generic;

namespace Stateflow.Data.Tests
{

	[TestFixture]
	public class ItemServiceTests
	{

		[Test]
		public void CreateNewItem(){

			var item = new Item<int> ();
			item.Name = "test";

			Assert.IsNotNull (item.FieldValues);
			Assert.IsFalse (item.IsDeleted);

			Assert.AreEqual ("test", item.Name);
		}

	}
}
