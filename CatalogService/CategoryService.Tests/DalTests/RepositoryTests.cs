using CatalogService.DAL.ContextKeeper;
using CatalogService.Domain.Interfaces;
using CategoryService.Tests.TestHelpers;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using CatalogService.Domain.Models;
using CatalogService.DAL.Models;
using CatalogService.DAL.Extensions;
using CatalogService.DAL.Interfaces;

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
            CollectionAssert.AllItemsAreInstancesOfType(actualItems.ToList(), typeof(ItemDtoModel));
            Assert.AreEqual(actualItems.Count(), expectedItems.Count());
            CollectionAssert.AllItemsAreUnique(actualItems.ToList());
        }

        [TestMethod]
        public void GetCategories()
        {
            // Arrange

            var sut = (ICategoryRepository)_serviceProvider.GetService(typeof(ICategoryRepository));
            var mapper = (ICategoryMapper)_serviceProvider.GetService(typeof(ICategoryMapper));
            List<CategoryDtoModel> expected = new();
            expected.Add(mapper.CategoryToDtoModel(_inMemoryDataStorage.Category1));
            expected.Add(mapper.CategoryToDtoModel(_inMemoryDataStorage.Category2));

            // Act
            var actual = sut.GetCategories();

            // Assert
            Assert.IsNotNull(actual);
            CollectionAssert.AllItemsAreNotNull(actual.ToList());
            CollectionAssert.AllItemsAreInstancesOfType(actual.ToList(), typeof(CategoryDtoModel));
            Assert.AreEqual(actual.Count(), expected.Count());
            CollectionAssert.AllItemsAreUnique(actual.ToList());
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
            Assert.IsInstanceOfType(actual, typeof(CategoryDtoModel));
            Assert.AreEqual(expected.Id, actual.Id);
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
            Assert.IsInstanceOfType(actual, typeof(ItemDtoModel));
            Assert.AreEqual(expected.Id, actual.Id);
        }

        [TestMethod]
        public void AddCategory()
        {
            // Arrange
            var sut = (ICategoryRepository)_serviceProvider.GetService(typeof(ICategoryRepository));
            var expectedCategory = ModelCreator.CreateCategoryDtoModel();

            // Act
            var result = sut.AddCategory(expectedCategory);

            // Assert
            var resultInDb = sut.GetCategory(expectedCategory.Id);

            Assert.IsTrue(result);
            Assert.IsInstanceOfType(resultInDb, typeof(CategoryDtoModel));
            Assert.AreEqual(expectedCategory.Id, resultInDb.Id);

            // Cleanup

            sut.DeleteCategory(expectedCategory.Id);
        }

        [TestMethod]
        public void AddItem()
        {
            // Arrange
            var sut = (IItemRepository)_serviceProvider.GetService(typeof(IItemRepository));
            var expectedItem = ModelCreator.CreateItemDtoModel();

            // Act
            var result = sut.AddItem(expectedItem);

            // Assert
            var resultInDb = sut.GetItem(expectedItem.Id);

            Assert.IsTrue(result);
            Assert.IsInstanceOfType(resultInDb, typeof(ItemDtoModel));
            Assert.AreEqual(expectedItem.Id, resultInDb.Id);

            // Cleanup

            sut.DeleteItem(expectedItem.Id);
        }

        [TestMethod]
        public void RemoveCategory()
        {
            // Arrange
            var sut = (ICategoryRepository)_serviceProvider.GetService(typeof(ICategoryRepository));
            var categoryToRemove = ModelCreator.CreateCategoryDtoModel();
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
            var itemToRemove = ModelCreator.CreateItemDtoModel();
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
            var categoryToUpdate = ModelCreator.CreateCategoryDtoModel();
            sut.AddCategory(categoryToUpdate);
            categoryToUpdate.Image = "New Image";

            // Act
            var result = sut.UpdateCategory(categoryToUpdate);

            // Assert

            var updatedCategory = sut.GetCategory(categoryToUpdate.Id);

            //Assert.IsTrue(result);
            Assert.AreEqual(categoryToUpdate.Image, updatedCategory.Image);

            // Cleanup 

            sut.DeleteCategory(categoryToUpdate.Id);
        }

        [TestMethod]
        public void UpdateItem()
        {
            // Arrange
            var sut = (IItemRepository)_serviceProvider.GetService(typeof(IItemRepository));
            var itemToUpdate = ModelCreator.CreateItemDtoModel();
            sut.AddItem(itemToUpdate);
            itemToUpdate.Price = 101;

            // Act
            var result = sut.UpdateItem(itemToUpdate);

            // Assert
            var updatedItem = sut.GetItem(itemToUpdate.Id);

            Assert.IsTrue(result);
            Assert.AreEqual(itemToUpdate.Id, updatedItem.Id);

            //Cleanup 

            sut.DeleteItem(itemToUpdate.Id);
        }

    }
}
