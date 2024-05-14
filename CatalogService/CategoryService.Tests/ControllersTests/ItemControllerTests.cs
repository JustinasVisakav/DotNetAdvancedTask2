using CatalogService.API.Controllers;
using CatalogService.BLL.Interfaces;
using CatalogService.Domain.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CategoryService.Tests.ControllersTests
{
    [TestClass]
    public class ItemControllerTests
    {
        [TestMethod]
        public void TestGetItems()
        {
            var mockCategoryService = new Mock<ICategoryService>();
            var mockItemService = new Mock<IItemService>();
            mockItemService.Setup(x=>x.GetItems()).Returns(new List<ItemDtoModel>() { new ItemDtoModel() });

            var itemController = new ItemController(mockItemService.Object, mockCategoryService.Object);

            var result = itemController.Get(Guid.Empty) as OkObjectResult;

            Assert.IsNotNull(result);
            Assert.AreEqual(200, result.StatusCode);
        }

        [TestMethod]
        public void TestGetItem()
        {
            var mockCategoryService = new Mock<ICategoryService>();
            var mockItemService = new Mock<IItemService>();
            mockItemService.Setup(x => x.GetItems()).Returns(new List<ItemDtoModel>() { new ItemDtoModel() });
            mockItemService.Setup(x => x.GetItem(It.IsAny<Guid>())).Returns(new ItemDtoModel());

            var itemController = new ItemController(mockItemService.Object, mockCategoryService.Object);

            var result = itemController.GetById(Guid.NewGuid()) as OkObjectResult;

            Assert.IsNotNull(result);
            Assert.AreEqual(200, result.StatusCode);
        }

        [TestMethod]
        public void TestDeleteItem()
        {
            var mockCategoryService = new Mock<ICategoryService>();
            var mockItemService = new Mock<IItemService>();
            mockItemService.Setup(x => x.GetItems()).Returns(new List<ItemDtoModel>() { new ItemDtoModel() });
            mockItemService.Setup(x => x.GetItem(It.IsAny<Guid>())).Returns(new ItemDtoModel());
            mockItemService.Setup(x => x.DeleteItem(It.IsAny<Guid>())).Returns(true);

            var itemController = new ItemController(mockItemService.Object, mockCategoryService.Object);

            var result = itemController.DeleteById(Guid.NewGuid()) as OkObjectResult;

            Assert.IsNotNull(result);
            Assert.AreEqual(200, result.StatusCode);
        }

        [TestMethod]
        public void TestUpdateItem()
        {
            var mockCategoryService = new Mock<ICategoryService>();
            var mockItemService = new Mock<IItemService>();
            mockItemService.Setup(x => x.GetItems()).Returns(new List<ItemDtoModel>() { new ItemDtoModel() });
            mockItemService.Setup(x => x.GetItem(It.IsAny<Guid>())).Returns(new ItemDtoModel());
            mockItemService.Setup(x => x.DeleteItem(It.IsAny<Guid>())).Returns(true);
            mockItemService.Setup(x=>x.UpdateItem(It.IsAny<ItemDtoModel>())).Returns(true);

            var itemController = new ItemController(mockItemService.Object, mockCategoryService.Object);

            var result = itemController.UpdateItem(new ItemDtoModel()) as OkObjectResult;

            Assert.IsNotNull(result);
            Assert.AreEqual(200, result.StatusCode);
        }

        [TestMethod]
        public void TestAddItem()
        {
            var mockCategoryService = new Mock<ICategoryService>();
            var mockItemService = new Mock<IItemService>();
            mockItemService.Setup(x => x.GetItems()).Returns(new List<ItemDtoModel>() { new ItemDtoModel() });
            mockItemService.Setup(x => x.GetItem(It.IsAny<Guid>())).Returns(new ItemDtoModel());
            mockItemService.Setup(x => x.DeleteItem(It.IsAny<Guid>())).Returns(true);
            mockItemService.Setup(x => x.UpdateItem(It.IsAny<ItemDtoModel>())).Returns(true);
            mockItemService.Setup(x => x.AddItem(It.IsAny<ItemDtoModel>())).Returns(true);

            var itemController = new ItemController(mockItemService.Object, mockCategoryService.Object);

            var result = itemController.AddItem(new ItemDtoModel()) as OkObjectResult;

            Assert.IsNotNull(result);
            Assert.AreEqual(200, result.StatusCode);
        }
    }
}
