using CatalogService.BLL.Services;
using CatalogService.DAL.Extensions;
using CatalogService.DAL.Models;
using CatalogService.Domain.Interfaces;
using CatalogService.Domain.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace CategoryService.Tests.BllTests
{
    [TestClass]
    public class ServicesTests
    {
        [TestMethod]
        public void ItemServiceGetItem()
        {
            //Arrange
            var itemRepositoryeMock = CreateItemRepositoryMock();
            var itemServoce = new ItemService(itemRepositoryeMock.Object);

            //Act
            var result = itemServoce.GetItem(Guid.NewGuid());

            //Assert
            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void ItemServiceGetItems()
        {
            //Arrange
            var itemRepositoryeMock = CreateItemRepositoryMock();
            var itemServoce = new ItemService(itemRepositoryeMock.Object);

            //Act
            var result = itemServoce.GetItems();

            //Assert
            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void ItemServiceDeleteItem()
        {
            //Arrange
            var itemRepositoryeMock = CreateItemRepositoryMock();
            var itemServoce = new ItemService(itemRepositoryeMock.Object);

            //Act
            var result = itemServoce.DeleteItem(Guid.NewGuid());

            //Assert
            Assert.IsTrue(result);
        }

        [TestMethod]
        public void ItemServiceAddItem()
        {
            //Arrange
            ItemDtoModel itemModel = new ItemDtoModel(); 
            var itemRepositoryeMock = CreateItemRepositoryMock();
            var itemServoce = new ItemService(itemRepositoryeMock.Object);

            //Act
            var result = itemServoce.AddItem(itemModel);

            //Assert
            Assert.IsTrue(result);
        }

        [TestMethod]
        public void ItemServiceUpdateItem()
        {
            //Arrange
            ItemDtoModel itemModel = new ItemDtoModel();
            var itemRepositoryeMock = CreateItemRepositoryMock();
            var itemServoce = new ItemService(itemRepositoryeMock.Object);

            //Act
            var result = itemServoce.UpdateItem(itemModel);

            //Assert
            Assert.IsTrue(result);
        }

        [TestMethod]
        public void CategoryServiceGetCategory()
        {
            //Arrange
            var categoryRepositoryeMock = CreateCategoryRepositoryMock();
            var categoryServoce = new CatalogService.BLL.Services.CategoryService(categoryRepositoryeMock.Object);

            //Act
            var result = categoryServoce.GetCategory(Guid.NewGuid());

            //Assert
            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void CategoryServiceGetCategories()
        {
            //Arrange
            var categoryRepositoryeMock = CreateCategoryRepositoryMock();
            var categoryServoce = new CatalogService.BLL.Services.CategoryService(categoryRepositoryeMock.Object);

            //Act
            var result = categoryServoce.GetCategories();

            //Assert
            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void CategoryServiceDeleteCategory()
        {
            //Arrange
            var categoryRepositoryeMock = CreateCategoryRepositoryMock();
            var categoryServoce = new CatalogService.BLL.Services.CategoryService(categoryRepositoryeMock.Object);

            //Act
            var result = categoryServoce.DeleteCategory(Guid.NewGuid());

            //Assert
            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void CategoryServiceAddCategory()
        {
            //Arrange
            CategoryDtoModel categoryModel = new CategoryDtoModel();
            var categoryRepositoryeMock = CreateCategoryRepositoryMock();
            var categoryServoce = new CatalogService.BLL.Services.CategoryService(categoryRepositoryeMock.Object);

            //Act
            var result = categoryServoce.AddCategory(categoryModel);

            //Assert
            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void CategoryServiceUpdateCategory()
        {
            //Arrange
            CategoryDtoModel categoryModel = new CategoryDtoModel();
            var categoryRepositoryeMock = CreateCategoryRepositoryMock();
            var categoryServoce = new CatalogService.BLL.Services.CategoryService(categoryRepositoryeMock.Object);

            //Act
            var result = categoryServoce.UpdateCategory(categoryModel);

            //Assert
            Assert.IsNotNull(result);
        }

        private Mock<IItemRepository> CreateItemRepositoryMock()
        {
            var itemModel = new ItemDtoModel();
            var itemModels = new List<ItemDtoModel>();
            itemModels.Add(itemModel);
            var itemRepositoryMock = new Mock<IItemRepository>();
            itemRepositoryMock.Setup(x => x.GetItem(It.IsAny<Guid>())).Returns(itemModel);
            itemRepositoryMock.Setup(x => x.UpdateItem(It.IsAny<ItemDtoModel>())).Returns(true);
            itemRepositoryMock.Setup(x => x.DeleteItem(It.IsAny<Guid>())).Returns(true);
            itemRepositoryMock.Setup(x => x.AddItem(It.IsAny<ItemDtoModel>())).Returns(true);
            itemRepositoryMock.Setup(x=>x.GetItems()).Returns(itemModels);

            return itemRepositoryMock;
        }

        private Mock<ICategoryRepository> CreateCategoryRepositoryMock()
        {
            var categoryModel = new CategoryDtoModel();
            var categoryModels = new List<CategoryDtoModel>();
            categoryModels.Add(categoryModel);
            var categoryMock = new Mock<ICategoryRepository>();
            categoryMock.Setup(x => x.GetCategory(It.IsAny<Guid>())).Returns(categoryModel);
            categoryMock.Setup(x => x.UpdateCategory(It.IsAny<CategoryDtoModel>())).Returns(true);
            categoryMock.Setup(x => x.DeleteCategory(It.IsAny<Guid>())).Returns(true);
            categoryMock.Setup(x => x.AddCategory(It.IsAny<CategoryDtoModel>())).Returns(true);
            categoryMock.Setup(x => x.GetCategories()).Returns(categoryModels);

            return categoryMock;
        }
    }
}
