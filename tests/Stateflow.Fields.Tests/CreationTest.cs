using System.Linq;
using NUnit.Framework;
using Stateflow.Fields.DataStores;

namespace Stateflow.Fields.Tests
{
    [TestFixture]
    public class CreationTest
    {
        [Test]
        public void StoreAndRetrieveTemplate()
        {
            var store = new InMemoryDataStore<int>();

            var ft = new Template<int>(store, "test")
            {
                Id = 1
            };
            ft.FieldDefinitions.Add(new FieldDefinition<int> { Name = "test", FieldType = FieldType.Amount });

            ft.Save();

            Assert.AreEqual(ft.Name, "test");
            Assert.IsTrue(ft.FieldDefinitions.Count == 1);

            // Load the 1
            var ft2 = store.LoadTemplate(1);

            Assert.AreEqual(ft.Name, ft2.Name);
            Assert.IsTrue(ft.FieldDefinitions.Count == ft2.FieldDefinitions.Count);
        }

        [Test]
        public void StoreAndRetrieveItem()
        {
            var store = new InMemoryDataStore<int>();
            var ft = new Template<int>(store, "test")
            {
                Id = 1
            };
            ft.FieldDefinitions.Add(new FieldDefinition<int> {Id=1, Name = "test", FieldType = FieldType.Amount });
            ft.FieldDefinitions.Add(new FieldDefinition<int> {Id=2, Name = "test2", FieldType = FieldType.SingleLineText, DefaultValue = "a"});

            var fi = ft.CreateNew();

            Assert.IsNotNull(fi);

            Assert.IsTrue(fi.Fields.Count==2);

            fi.Id = 1;

            // Make at least one change, non dirty items are not stored
            fi["test"] = "value";

            fi["test2"] = "b";
            fi.Save();

            var fi2 = store.LoadItem(1);

            Assert.AreEqual(fi2.Fields.Count, fi.Fields.Count);

        }

        [Test]
        public void SetupATemplate()
        {
            var ft = new Template<int>(new InMemoryDataStore<int>(), "test");

            Assert.IsNotNull(ft);
            Assert.AreEqual(ft.Name, "test");

            ft.FieldDefinitions.Add(new FieldDefinition<int>() { Name = "test", FieldType = FieldType.Amount });

            Assert.IsTrue(ft.FieldDefinitions.Count == 1);

            var fi = ft.CreateNew();

            Assert.IsNotNull(fi);

            Assert.IsTrue(fi.Fields.Count == 1);
        }

        [Test]
        public void ReactOnFieldChangesUsingEvent()
        {
            var ft = new Template<int>(new InMemoryDataStore<int>(), "test");

            var fd = new FieldDefinition<int>() { Name = "test", FieldType = FieldType.Amount };

            ft.FieldDefinitions.Add(fd);

            var fi = ft.CreateNew();

            fi.Fields["test"].ValueChanged += (sender, newValue) => Assert.AreEqual(newValue, "test");
            fi.Fields["test"].Value = "test";

            Assert.AreEqual(fi.Fields["test"].Value, "test");

        }

        [Test]
        public void CanCreateFieldDefinition()
        {
            var fd = new FieldDefinition<int>();
            fd.Id = 1;
            fd.Name = "test";

            Assert.AreEqual(fd.Id, 1);
            Assert.AreEqual(fd.Name, "test");
        }

        [Test]
        public void CanAddFieldDefinitions()
        {
            var fd = new FieldDefinition<int>();
            fd.Id = 1;

            var fdc = new FieldDefinitionCollection<int>();
            fdc.Add(fd);

            Assert.IsTrue(fdc.Count == 1);
            Assert.AreSame(fdc.First().Value, fd);
            Assert.AreSame(fdc[1], fd);
        }

        [Test]
        public void CanCreateAFieldsItemAndAssingValue()
        {

            // Create a template instance which is the template for the fields.
            var fit = new Template<int>(new InMemoryDataStore<int>());

            // Create a new simple field definition
            var fd = new FieldDefinition<int>();
            fd.Id = 1;
            fd.Name = "test-field";
            fd.FieldType = FieldType.SingleLineText;


            // And add the field definition to the template
            fit.FieldDefinitions.Add(fd);

            // Create a fields item which is based on the template
            var fi = new FieldsItem<int>(fit);
            fi.Id = 1;

            // Get the field using its id
            var f = fi.Fields[1];

            // Get the field using its name
            var fName = fi.Fields["test-field"];

            Assert.AreEqual(f, fName, "Should have returned the same item.");
            Assert.AreEqual(f.Id, fd.Id);
            Assert.IsFalse(fi.IsDirty);

            // Lets set some value
            f.Value = "test";

            Assert.AreEqual(f.Value, "test");
            Assert.AreEqual(f.OriginalValue, null);
            Assert.AreEqual(fi.Fields[f.Id].Value, "test");
            Assert.IsTrue(fi.IsDirty);

            // Save the data in the item
            fi.Save();

            // Get the list of revisions.
            var revisions = fi.Revisions;

            // A new revision should be created
            Assert.IsNotNull(revisions);
            Assert.IsTrue(revisions.Count > 0, "Since we did a save, there should be a new revision");

            Assert.IsNotNull(revisions[1]);

            //foreach (var revision in revisions) {
            //	Assert.IsNotNull (revision.Value.Fields [1].Value);
            //}				


        }
    }

}

