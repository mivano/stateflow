using NUnit.Framework;
using Moq;
using System;
using System.Collections.Generic;
using Stateflow.Data.Store.InMemory;
using System.Linq;

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

		[Test]
		public void CreateItemBasedOnTemplate(){

			var template = new Template<int> ();
			template.Name ="test-template";
			template.FieldDefinitions.Add (new TemplateFieldDefinition<int>{ FieldDefinition = 1,  IsRequired = false });



		}
	}

}
