using CatalogService.API.Controllers;
using CatalogService.BLL.Interfaces;
using CatalogService.Domain.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace CategoryService.Tests.ControllersTests
{
    [TestClass]
    public class CategoryControllerTest
    {
        [TestMethod]
        public void TestDeleteCategory()
        {
            var mockCategoryService = new Mock<ICategoryService>();
            mockCategoryService.Setup(x => x.GetCategory(It.IsAny<Guid>())).Returns(new CategoryDtoModel());
            mockCategoryService.Setup(x=>x.DeleteCategory(It.IsAny<Guid>())).Returns(true);
            var mockItemService = new Mock<IItemService>();
            var categoryController = new CategoryController(mockCategoryService.Object, mockItemService.Object);
            var result = categoryController.DeleteCategory(Guid.NewGuid()) as OkObjectResult;

            Assert.IsNotNull(result);
            Assert.AreEqual(200, result.StatusCode);
            Assert.AreEqual(true, result.Value);
        }

        [TestMethod]
        public void TestGetCategories()
        {
            var mockCategoryService = new Mock<ICategoryService>();
            mockCategoryService.Setup(x => x.GetCategory(It.IsAny<Guid>())).Returns(new CategoryDtoModel());
            mockCategoryService.Setup(x => x.GetCategories()).Returns(new List<CategoryDtoModel>() { new CategoryDtoModel()});
            mockCategoryService.Setup(x => x.DeleteCategory(It.IsAny<Guid>())).Returns(true);
            var mockItemService = new Mock<IItemService>();
            var categoryController = new CategoryController(mockCategoryService.Object, mockItemService.Object);
            var result = categoryController.Get() as OkObjectResult;

            Assert.IsNotNull(result);
            Assert.AreEqual(200, result.StatusCode);
        }

        [TestMethod]
        public void TestGetCategory()
        {
            var mockCategoryService = new Mock<ICategoryService>();
            mockCategoryService.Setup(x => x.GetCategory(It.IsAny<Guid>())).Returns(new CategoryDtoModel());
            mockCategoryService.Setup(x => x.GetCategories()).Returns(new List<CategoryDtoModel>() { new CategoryDtoModel() });
            mockCategoryService.Setup(x => x.DeleteCategory(It.IsAny<Guid>())).Returns(true);
            var mockItemService = new Mock<IItemService>();
            var categoryController = new CategoryController(mockCategoryService.Object, mockItemService.Object);
            var result = categoryController.GetCategory(Guid.NewGuid()) as OkObjectResult;

            Assert.IsNotNull(result);
            Assert.AreEqual(200, result.StatusCode);
        }

        [TestMethod]
        public void TestUpdateCategory()
        {
            var mockCategoryService = new Mock<ICategoryService>();
            mockCategoryService.Setup(x => x.GetCategory(It.IsAny<Guid>())).Returns(new CategoryDtoModel());
            mockCategoryService.Setup(x => x.GetCategories()).Returns(new List<CategoryDtoModel>() { new CategoryDtoModel() });
            mockCategoryService.Setup(x => x.DeleteCategory(It.IsAny<Guid>())).Returns(true);
            mockCategoryService.Setup(x=>x.UpdateCategory(It.IsAny<CategoryDtoModel>())).Returns(true);
            var mockItemService = new Mock<IItemService>();
            var categoryController = new CategoryController(mockCategoryService.Object, mockItemService.Object);
            var result = categoryController.Update(new CategoryDtoModel()) as OkObjectResult;

            Assert.IsNotNull(result);
            Assert.AreEqual(200, result.StatusCode);
        }

        [TestMethod]
        public void TestPostCategory()
        {
            var mockCategoryService = new Mock<ICategoryService>();
            mockCategoryService.Setup(x => x.GetCategory(It.IsAny<Guid>())).Returns(new CategoryDtoModel());
            mockCategoryService.Setup(x => x.GetCategories()).Returns(new List<CategoryDtoModel>() { new CategoryDtoModel() });
            mockCategoryService.Setup(x => x.DeleteCategory(It.IsAny<Guid>())).Returns(true);
            mockCategoryService.Setup(x => x.UpdateCategory(It.IsAny<CategoryDtoModel>())).Returns(true);
            mockCategoryService.Setup(x => x.AddCategory(It.IsAny<CategoryDtoModel>())).Returns(true);
            var mockItemService = new Mock<IItemService>();
            var categoryController = new CategoryController(mockCategoryService.Object, mockItemService.Object);
            var result = categoryController.Post(new CategoryDtoModel()) as OkObjectResult;

            Assert.IsNotNull(result);
            Assert.AreEqual(200, result.StatusCode);
        }
    }
}
