using NUnit.Framework;
using System;
using System.Linq;
using System.Collections.Generic;

namespace Stateflow.Fields.Tests
{

	[TestFixture]
	public class ValidatorTest{

		[Test()]
		public void ValidateNotEmpty(){
			var f = new GenericField<int> ();
			f.FieldType = FieldType.SingleLineText;

			f.Validators = new List<IFieldValidator<int>> {
				new NotEmptyValidator<int>()
			};

			Assert.IsFalse (f.IsValid ());

			f.Value = "test";

			Assert.IsTrue (f.IsValid ());

		}

		[Test()]
		public void ValidateNotNull(){
			var f = new GenericField<int> ();
			f.FieldType = FieldType.SingleLineText;

			f.Validators = new List<IFieldValidator<int>> {
				new NotNullValidator<int>()
			};

			Assert.IsFalse (f.IsValid ());

			f.Value = "test";

			Assert.IsTrue (f.IsValid ());

		}

		[Test()]
		public void MinLengthInCollection(){
			var c = new FieldCollection<int> ();
			c.Validators=new List<IFieldValidator<int>> {
				new MinimumItemsInCollectionValidator<int>(1)
			};



		}
	}
}
