using CartingService.BLL.Models;
using CartingService.BLL.Services;
using CartingService.DAL.Interfaces;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace CartingService.Tests.BblTests
{
    [TestClass]
    public class CartServiceTests
    {
        [TestMethod]
        public void AddToCartCart_ShouldReturnTrue()
        {
            //Arrange
            var cartModel = new CartModel();
            var mockCartRepository = new Mock<IGenericRepository<CartModel>>();
            mockCartRepository.Setup(x => x.GetRecord(It.IsAny<Guid>())).Returns(cartModel);
            mockCartRepository.Setup(x => x.UpsertRecord(It.IsAny<CartModel>())).Returns(true);
            List<ItemModel> items = new List<ItemModel>();

            items.Add(new ItemModel());

            var repository = new CartService(mockCartRepository.Object);

            //Act
            bool result = repository.AddToCartCart(Guid.NewGuid(), items);

            //Assert
            Assert.IsTrue(result);
        }

        [TestMethod]
        public void CreateNewCart_ShouldReturnGuidOfNewCart()
        {
            //Arrange
            var cartModel = new CartModel();
            var mockCartRepository = new Mock<IGenericRepository<CartModel>>();
            mockCartRepository.Setup(x => x.UpsertRecord(It.IsAny<CartModel>())).Returns(true);

            var repository = new CartService(mockCartRepository.Object);

            //Act
            Guid result = repository.CreateNewCart();

            //Assert
            Assert.IsFalse(string.IsNullOrEmpty(result.ToString()));
        }

        [TestMethod]
        public void GetCart_ShouldReturnCart()
        {
            //Arrange
            var cartModel = new CartModel();
            var mockCartRepository = new Mock<IGenericRepository<CartModel>>();
            mockCartRepository.Setup(x => x.GetRecord(It.IsAny<Guid>())).Returns(cartModel);

            var repository = new CartService(mockCartRepository.Object);

            //Act
            CartModel result = repository.GetCart(Guid.NewGuid());

            //Assert
            Assert.IsNotNull(result);
            Assert.IsFalse(string.IsNullOrEmpty(result.Id.ToString()));
            Assert.IsNotNull(result.Items);
        }

        [TestMethod]
        public void RemoveFromCart_ShouldReturnTrue()
        {
            //Arrange
            List<ItemModel> items = new List<ItemModel>();
            ItemModel item = new ItemModel();
            items.Add(item);
            var cartModel = new CartModel();
            cartModel.Items.Add(item);
            var mockCartRepository = new Mock<IGenericRepository<CartModel>>();
            mockCartRepository.Setup(x => x.GetRecord(It.IsAny<Guid>())).Returns(cartModel);
            mockCartRepository.Setup(x => x.UpsertRecord(It.IsAny<CartModel>())).Returns(true);
            

            var repository = new CartService(mockCartRepository.Object);

            //Act
            bool result = repository.RemoveFromCart(cartModel.Id, items);

            //Assert
            Assert.IsTrue(result);
        }
    }
}
