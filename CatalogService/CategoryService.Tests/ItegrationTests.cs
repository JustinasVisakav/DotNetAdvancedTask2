﻿using CatalogService.BLL.Interfaces;
using CatalogService.DAL.ContextKeeper;
using CatalogService.DAL.Extensions;
using CatalogService.DAL.Interfaces;
using CategoryService.Tests.DalTests;
using CategoryService.Tests.TestHelpers;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CategoryService.Tests
{
    [TestClass]
    public class ItegrationTests
    {
        private static IServiceProvider _serviceProvider;
        private static DatabaseContext _context;
        private static DalTestDataStorage _inMemoryDataStorage;
        private static ICategoryService _categoryService;
        private static IItemService _itemService;
        private static IItemMapper _itemMapper;
        private static ICategoryMapper _categoryMapper;

        [ClassInitialize]
        public static void ClassInitialize(TestContext context)
        {
            _inMemoryDataStorage = new DalTestDataStorage();
            _serviceProvider = ServiceCreatorTestHelper.GetServiceProvider();
            _context = _serviceProvider.GetRequiredService<DatabaseContext>();
            DalTestDataStorage.FillTheDatabaseWithTestData(_context, _inMemoryDataStorage);
            _categoryService = (ICategoryService)_serviceProvider.GetService(typeof(ICategoryService));
            _itemService = (IItemService)_serviceProvider.GetService(typeof(IItemService));
            _itemMapper = (IItemMapper)_serviceProvider.GetService(typeof(IItemMapper));
            _categoryMapper = (ICategoryMapper)_serviceProvider.GetService(typeof(ICategoryMapper));
        }

        [TestMethod]
        public void GetCategory()
        {
            var result = _categoryService.GetCategory(_inMemoryDataStorage.Category1.Id);

            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void GetCategories()
        {
            var result = _categoryService.GetCategories();

            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void AddCategory()
        {
            var newCategory = ModelCreator.CreateCategoryDtoModel();

            var result = _categoryService.AddCategory(newCategory);

            Assert.IsTrue(result);
        }

        [TestMethod]
        public void RemoveCategory()
        {
            var newCategory = ModelCreator.CreateCategoryDtoModel();

            _categoryService.AddCategory(newCategory);

            var result = _categoryService.DeleteCategory(newCategory.Id);

            Assert.IsTrue(result);
        }

        [TestMethod]
        public void UpdateCategory()
        {
            var categoryToUpdate = _categoryMapper.CategoryToDtoModel(_inMemoryDataStorage.Category2);

            categoryToUpdate.Image = "New Image";

            var result = _categoryService.UpdateCategory(categoryToUpdate);

            Assert.IsTrue(result);
        }


        [TestMethod]
        public void GetItem()
        {
            var itemToGet = _inMemoryDataStorage.Item1;
            var result = _itemService.GetItem(itemToGet.Id);
            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void GetItems()
        {
            var result = _itemService.GetItems();
            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void AddItem()
        {
            var itemToAdd = ModelCreator.CreateItemDtoModel();
            var result = _itemService.AddItem(itemToAdd);
            Assert.IsTrue(result);
        }

        [TestMethod]
        public void RemoveItem()
        {
            var newItem = ModelCreator.CreateItemDtoModel();
            _itemService.AddItem(newItem);
            var result = _itemService.DeleteItem(newItem.Id);
            Assert.IsTrue(result);
        }

        [TestMethod]
        public void UpdateItem()
        {
            var itemToUpdate = _itemMapper.ItemToItemDtoModel(_inMemoryDataStorage.Item2);
            itemToUpdate.Name = "New name";
            var result = _itemService.UpdateItem(itemToUpdate);
            Assert.IsTrue(result);
        }
    }
}
