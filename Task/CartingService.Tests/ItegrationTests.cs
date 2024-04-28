using CartingService.BLL.Interfaces;
using CartingService.BLL.Models;
using CartingService.Tests.Helpers;
using LiteDB;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CartingService.Tests
{
    [TestClass]
    public class ItegrationTests
    {
        private static IServiceProvider serviceProvider;
        private static ICartService cartService;

        [ClassInitialize]
        public static void ClassInitialize(TestContext context)
        {
            serviceProvider = IntegrationTestsHelper.GetServiceProvider();
            cartService = serviceProvider.GetRequiredService<ICartService>();
        }

        [TestInitialize]
        public void TestInitialize()
        {
            var db = serviceProvider.GetService<LiteDatabase>();
            db.GetCollection<CartModel>("Cart").DeleteAll();
        }


        [TestMethod]
        public void CreateNewCartAndGetThatCart()
        {
            //Act
            var newCartId = cartService.CreateNewCart();
            var newCart = cartService.GetCart(newCartId);

            //Assert
            Assert.IsFalse(string.IsNullOrEmpty(newCartId.ToString()));
        }

        [TestMethod]
        public void AddItemToCart()
        {
            //Arrange
            var newCartId = cartService.CreateNewCart();
            var newItem = new ItemModel();
            List<ItemModel> items = new List<ItemModel> { newItem };

            //Act
            var result = cartService.AddToCartCart(newCartId, items);

            //Assert
            var cart = cartService.GetCart(newCartId);

            Assert.IsTrue(cart.Items.Count >0);
        }

        [TestMethod]
        public void removeFromCartTest()
        {
            //Arrange
            var newCartId = cartService.CreateNewCart();
            var newItem = new ItemModel();
            List<ItemModel> items = new List<ItemModel> { newItem };
            cartService.AddToCartCart(newCartId, items);

            //Act
            var result = cartService.RemoveFromCart(newCartId, items);

            //Assert
            var cart = cartService.GetCart(newCartId);
            Assert.IsTrue(cart.Items.Count ==0);
            
        }
    }
}
