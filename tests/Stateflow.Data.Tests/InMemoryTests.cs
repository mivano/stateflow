using NUnit.Framework;
using Moq;
using System;
using System.Collections.Generic;
using Stateflow.Data.Store.InMemory;
using System.Linq;

namespace Stateflow.Data.Tests
{

	[TestFixture]
	public class InMemoryTests
	{

		[Test]
		public void CanStoreFieldDefinitionsInMemory()
		{

			var fieldsService = new FieldDefinitionService<int> (new InMemoryFieldDefinitionRepository<int> ());
			var fields = new [] {
				new FieldDefinition<int>{ FieldType = FieldType.Amount, Id = 1, Name = "Amount" },
				new FieldDefinition<int>{ FieldType = FieldType.SingleLineText, Id = 2, Name = "Text" }
			};

			foreach (var field in fields) {
				fieldsService.SaveAsync (field);
			}

			Assert.IsTrue (fieldsService.GetAll ().Result.Count () == 2);
			Assert.IsTrue (fieldsService.GetByIdAsync (1).Result.Name == "Amount");
			Assert.IsTrue (fieldsService.GetByIdAsync (1).Result.FieldType == FieldType.Amount);

			fieldsService.DeleteAsync (1);

			Assert.IsTrue (fieldsService.GetAll ().Result.Count () == 1);

		}

		[Test]
		public void CanStoreTemplatesInMemory()
		{

			var fieldsService = new FieldDefinitionService<int> (new InMemoryFieldDefinitionRepository<int> ());
			var fields = new [] {
				new FieldDefinition<int>{ FieldType = FieldType.Amount, Id = 1, Name = "Amount" },
				new FieldDefinition<int>{ FieldType = FieldType.SingleLineText, Id = 2, Name = "Text" }
			};

			foreach (var field in fields) {
				fieldsService.SaveAsync (field);
			}

			var templatesService = new TemplateService<int> (new InMemoryTemplateRepository<int> ());
			var template = new Template<int> ();
			template.Id = 1;
			template.Name = "template-1";
			template.FieldDefinitions.Add (new TemplateFieldDefinition<int>{ FieldDefinition = 1, IsRequired = false });

			templatesService.SaveAsync (template);

			Assert.IsNotNull (templatesService.GetByIdAsync (1).Result);
			Assert.IsNotNull (templatesService.GetByNameAsync ("template-1").Result);

			templatesService.DeleteAsync (1);

			Assert.IsNull (templatesService.GetByIdAsync (1).Result);
			Assert.IsNull (templatesService.GetByNameAsync ("template-1").Result);
		}

		[Test]
		public void CanStoreItemsInMemory(){

			// Build up base data
			var fieldsService = new FieldDefinitionService<int> (new InMemoryFieldDefinitionRepository<int> ());
			var fields = new [] {
				new FieldDefinition<int>{ FieldType = FieldType.Amount, Id = 1, Name = "Amount" },
				new FieldDefinition<int>{ FieldType = FieldType.SingleLineText, Id = 2, Name = "Text" }
			};

			foreach (var field in fields) {
				fieldsService.SaveAsync (field);
			}

			var templatesService = new TemplateService<int> (new InMemoryTemplateRepository<int> ());
			var template = new Template<int> ();
			template.Id = 1;
			template.Name = "template-1";
			template.FieldDefinitions.Add (new TemplateFieldDefinition<int>{ FieldDefinition = 1, IsRequired = false });

			templatesService.SaveAsync (template);

			var itemsService = new ItemService<int> (new InMemoryItemsRepository<int> ());
			var fieldValues = new List<FieldValue<int>> ();
			fieldValues.Add (new FieldValue<int> { 
				FieldDefinitionId = 1,
				Data = new FieldData<int> {
					Value = 4.5
				}
			});
			fieldValues.Add (new FieldValue<int> { 
				FieldDefinitionId = 2,
				Data = new FieldData<int> {
					Value = "text"
				}
			});
			var item = itemsService.CreateAsync (1, fieldValues).Result;

			Assert.IsNotNull (item);
			Assert.IsTrue (item.Identifier.Revision == 1);
			Assert.IsTrue (item.Identifier.TemplateId == 1);
			Assert.IsTrue (item.FieldValues.Count () == 2);


		}
	}
}
