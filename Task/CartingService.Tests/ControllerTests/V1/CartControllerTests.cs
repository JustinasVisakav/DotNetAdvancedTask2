using CartingService.BLL.Interfaces;
using CartingService.BLL.Models;
using CatingService.Controllers.V1;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CartingService.Tests.ControllerTests.V1
{
    [TestClass]
    public class CartControllerTests
    {
        [TestMethod]
        public void TestGetCart()
        {
            var mockCategoryService = new Mock<ICartService>();
            mockCategoryService.Setup(x => x.GetCart(It.IsAny<Guid>())).Returns(new CartModel());

            var cartController = new CartController(mockCategoryService.Object);
            var result = cartController.Get(Guid.NewGuid()) as OkObjectResult;

            Assert.IsNotNull(result);
            Assert.AreEqual(200, result.StatusCode);
        }

        [TestMethod]
        public void TestCreateCart()
        {
            var mockCategoryService = new Mock<ICartService>();
            mockCategoryService.Setup(x => x.GetCart(It.IsAny<Guid>())).Returns(new CartModel());
            mockCategoryService.Setup(x => x.CreateNewCart()).Returns(Guid.NewGuid());

            var cartController = new CartController(mockCategoryService.Object);
            var result = cartController.Create() as OkObjectResult;

            Assert.IsNotNull(result);
            Assert.AreEqual(200, result.StatusCode);
        }

        [TestMethod]
        public void TestAddToCartCart()
        {
            var mockCategoryService = new Mock<ICartService>();
            mockCategoryService.Setup(x => x.GetCart(It.IsAny<Guid>())).Returns(new CartModel());
            mockCategoryService.Setup(x => x.CreateNewCart()).Returns(Guid.NewGuid());
            mockCategoryService.Setup(x => x.AddToCartCart(It.IsAny<Guid>(), It.IsAny<List<ItemModel>>())).Returns(true);

            var cartController = new CartController(mockCategoryService.Object);
            var result = cartController.AddToCart(Guid.NewGuid(),new List<ItemModel>() { new ItemModel()}) as OkObjectResult;

            Assert.IsNotNull(result);
            Assert.AreEqual(200, result.StatusCode);
        }

        [TestMethod]
        public void TestDeleteCart()
        {
            var mockCategoryService = new Mock<ICartService>();
            mockCategoryService.Setup(x => x.GetCart(It.IsAny<Guid>())).Returns(new CartModel());
            mockCategoryService.Setup(x => x.CreateNewCart()).Returns(Guid.NewGuid());
            mockCategoryService.Setup(x => x.AddToCartCart(It.IsAny<Guid>(), It.IsAny<List<ItemModel>>())).Returns(true);
            mockCategoryService.Setup(x=>x.RemoveFromCart(It.IsAny<Guid>(),It.IsAny<List<ItemModel>>())).Returns(true);

            var cartController = new CartController(mockCategoryService.Object);
            var result = cartController.Delete(Guid.NewGuid(), new List<ItemModel>() { new ItemModel() }) as OkObjectResult;

            Assert.IsNotNull(result);
            Assert.AreEqual(200, result.StatusCode);
        }
    }
}
