using CatalogService.BLL.Services;
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
            ItemModel itemModel = new ItemModel(); 
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
            ItemModel itemModel = new ItemModel();
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
            var categoryServoce = new CategoryServiceService(categoryRepositoryeMock.Object);

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
            var categoryServoce = new CategoryServiceService(categoryRepositoryeMock.Object);

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
            var categoryServoce = new CategoryServiceService(categoryRepositoryeMock.Object);

            //Act
            var result = categoryServoce.DeleteCategory(Guid.NewGuid());

            //Assert
            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void CategoryServiceAddCategory()
        {
            //Arrange
            CategoryModel categoryModel = new CategoryModel();
            var categoryRepositoryeMock = CreateCategoryRepositoryMock();
            var categoryServoce = new CategoryServiceService(categoryRepositoryeMock.Object);

            //Act
            var result = categoryServoce.AddCategory(categoryModel);

            //Assert
            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void CategoryServiceUpdateCategory()
        {
            //Arrange
            CategoryModel categoryModel = new CategoryModel();
            var categoryRepositoryeMock = CreateCategoryRepositoryMock();
            var categoryServoce = new CategoryServiceService(categoryRepositoryeMock.Object);

            //Act
            var result = categoryServoce.UpdateCategory(categoryModel);

            //Assert
            Assert.IsNotNull(result);
        }

        private Mock<IItemRepository> CreateItemRepositoryMock()
        {
            var itemModel = new ItemModel();
            var itemModels = new List<ItemModel>();
            itemModels.Add(itemModel);
            var itemRepositoryMock = new Mock<IItemRepository>();
            itemRepositoryMock.Setup(x => x.GetItem(It.IsAny<Guid>())).Returns(itemModel);
            itemRepositoryMock.Setup(x => x.UpdateItem(It.IsAny<ItemModel>())).Returns(true);
            itemRepositoryMock.Setup(x => x.DeleteItem(It.IsAny<Guid>())).Returns(true);
            itemRepositoryMock.Setup(x => x.AddItem(It.IsAny<ItemModel>())).Returns(true);
            itemRepositoryMock.Setup(x=>x.GetItems()).Returns(itemModels);

            return itemRepositoryMock;
        }

        private Mock<ICategoryRepository> CreateCategoryRepositoryMock()
        {
            var categoryModel = new CategoryModel();
            var categoryModels = new List<CategoryModel>();
            categoryModels.Add(categoryModel);
            var categoryMock = new Mock<ICategoryRepository>();
            categoryMock.Setup(x => x.GetCategory(It.IsAny<Guid>())).Returns(categoryModel);
            categoryMock.Setup(x => x.UpdateCategory(It.IsAny<CategoryModel>())).Returns(true);
            categoryMock.Setup(x => x.DeleteCategory(It.IsAny<Guid>())).Returns(true);
            categoryMock.Setup(x => x.AddCategory(It.IsAny<CategoryModel>())).Returns(true);
            categoryMock.Setup(x => x.GetCategories()).Returns(categoryModels);

            return categoryMock;
        }
    }
}
