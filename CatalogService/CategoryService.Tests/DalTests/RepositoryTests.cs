using CatalogService.Domain.ContextKeeper;
using CatalogService.Domain.Interfaces;
using CategoryService.Tests.TestHelpers;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using CatalogService.Domain.Models;

namespace CategoryService.Tests.DalTests
{
    [TestClass]
    public class RepositoryTests
    {
        private static IServiceProvider _serviceProvider;
        private static DatabaseContext _context;
        private static DalTestDataStorage _inMemoryDataStorage;


        [ClassInitialize]
        public static void ClassInitialize(TestContext context)
        {
            _inMemoryDataStorage = new DalTestDataStorage();
            _serviceProvider = ServiceCreatorTestHelper.GetServiceProvider();
            _context = _serviceProvider.GetRequiredService<DatabaseContext>();
            DalTestDataStorage.FillTheDatabaseWithTestData(_context, _inMemoryDataStorage);
        }

        [TestMethod]
        public void GetItems()
        {
            // Arrange

            var sut = (IItemRepository)_serviceProvider.GetService(typeof(IItemRepository));
            List<ItemModel> expectedItems = new();
            expectedItems.Add(_inMemoryDataStorage.Item1);
            expectedItems.Add(_inMemoryDataStorage.Item2);

            // Act
            var actualItems = sut.GetItems();

            // Assert
            Assert.IsNotNull(actualItems);
            CollectionAssert.AllItemsAreNotNull(actualItems.ToList());
            CollectionAssert.AllItemsAreInstancesOfType(actualItems.ToList(), typeof(ItemModel));
            Assert.AreEqual(actualItems.Count(), expectedItems.Count());
            CollectionAssert.AllItemsAreUnique(actualItems.ToList());
            CollectionAssert.AreEquivalent(actualItems.ToList(), expectedItems.ToList());
        }

        [TestMethod]
        public void GetCategories()
        {
            // Arrange

            var sut = (ICategoryRepository)_serviceProvider.GetService(typeof(ICategoryRepository));
            List<CategoryModel> expected = new();
            expected.Add(_inMemoryDataStorage.Category1);
            expected.Add(_inMemoryDataStorage.Category2);

            // Act
            var actual = sut.GetCategories();

            // Assert
            Assert.IsNotNull(actual);
            CollectionAssert.AllItemsAreNotNull(actual.ToList());
            CollectionAssert.AllItemsAreInstancesOfType(actual.ToList(), typeof(CategoryModel));
            Assert.AreEqual(actual.Count(), expected.Count());
            CollectionAssert.AllItemsAreUnique(actual.ToList());
            CollectionAssert.AreEquivalent(actual.ToList(), expected.ToList());
        }

        [TestMethod]
        public void GetCategoryById()
        {
            // Arrange
            var sut = (ICategoryRepository)_serviceProvider.GetService(typeof(ICategoryRepository));
            var expected = _inMemoryDataStorage.Category1;

            // Act
            var actual = sut.GetCategory(expected.Id);

            // Assert
            Assert.IsNotNull(actual);
            Assert.IsInstanceOfType(actual, typeof(CategoryModel));
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void GetItemById()
        {
            // Arrange
            var sut = (IItemRepository)_serviceProvider.GetService(typeof(IItemRepository));
            var expected = _inMemoryDataStorage.Item1;

            // Act
            var actual = sut.GetItem(expected.Id);

            // Assert
            Assert.IsNotNull(actual);
            Assert.IsInstanceOfType(actual, typeof(ItemModel));
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void AddCategory()
        {
            // Arrange
            var sut = (ICategoryRepository)_serviceProvider.GetService(typeof(ICategoryRepository));
            var expectedCategory = ModelCreator.CreateCategoryModel();

            // Act
            var result = sut.AddCategory(expectedCategory);

            // Assert
            var resultInDb = sut.GetCategory(expectedCategory.Id);

            Assert.IsTrue(result);
            Assert.IsInstanceOfType(resultInDb, typeof(CategoryModel));
            Assert.AreEqual(expectedCategory, resultInDb);

            // Cleanup

            sut.DeleteCategory(expectedCategory.Id);
        }

        [TestMethod]
        public void AddItem()
        {
            // Arrange
            var sut = (IItemRepository)_serviceProvider.GetService(typeof(IItemRepository));
            var expectedItem = ModelCreator.CreateItemModel();

            // Act
            var result = sut.AddItem(expectedItem);

            // Assert
            var resultInDb = sut.GetItem(expectedItem.Id);

            Assert.IsTrue(result);
            Assert.IsInstanceOfType(resultInDb, typeof(ItemModel));
            Assert.AreEqual(expectedItem, resultInDb);

            // Cleanup

            sut.DeleteItem(expectedItem.Id);
        }

        [TestMethod]
        public void RemoveCategory()
        {
            // Arrange
            var sut = (ICategoryRepository)_serviceProvider.GetService(typeof(ICategoryRepository));
            var categoryToRemove = ModelCreator.CreateCategoryModel();
            sut.AddCategory(categoryToRemove);

            // Act
            var result = sut.DeleteCategory(categoryToRemove.Id);

            // Assert

            Assert.IsTrue(result);
        }

        [TestMethod]
        public void RemoveItem()
        {
            // Arrange
            var sut = (IItemRepository)_serviceProvider.GetService(typeof(IItemRepository));
            var itemToRemove = ModelCreator.CreateItemModel();
            sut.AddItem(itemToRemove);

            // Act
            var result = sut.DeleteItem(itemToRemove.Id);

            // Assert

            Assert.IsTrue(result);
        }

        [TestMethod]
        public void UpdateCategory()
        {
            // Arrange
            var sut = (ICategoryRepository)_serviceProvider.GetService(typeof(ICategoryRepository));
            var categoryToUpdate = ModelCreator.CreateCategoryModel();
            sut.AddCategory(categoryToUpdate);
            categoryToUpdate.Image = "New Image";

            // Act
            var result = sut.UpdateCategory(categoryToUpdate);

            // Assert

            var updatedCategory = sut.GetCategory(categoryToUpdate.Id);

            Assert.IsTrue(result);
            Assert.AreEqual(categoryToUpdate,updatedCategory);

            // Cleanup 

            sut.DeleteCategory(categoryToUpdate.Id);
        }

        [TestMethod]
        public void UpdateItem()
        {
            // Arrange
            var sut = (IItemRepository)_serviceProvider.GetService(typeof(IItemRepository));
            var itemToUpdate = ModelCreator.CreateItemModel();
            sut.AddItem(itemToUpdate);
            itemToUpdate.Price = 101;

            // Act
            var result = sut.UpdateItem(itemToUpdate);

            // Assert
            var updatedItem = sut.GetItem(itemToUpdate.Id);

            Assert.IsTrue(result);
            Assert.AreEqual(itemToUpdate,updatedItem);

            //Cleanup 

            sut.DeleteItem(itemToUpdate.Id);
        }

    }
}
