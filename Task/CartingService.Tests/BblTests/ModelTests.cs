using CartingService.BLL.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CartingService.Tests.BblTests
{
    [TestClass]
    public class ModelTests
    {
        [TestMethod]
        public void CreatingNewCart_ItemListShouldBeInitiated()
        {
            CartModel cart = new CartModel();

            Assert.IsNotNull(cart.Items);
        }

        [TestMethod]
        public void CreatingNewCart_CartIdShouldBeCreated()
        {
            CartModel cart = new CartModel();

            Assert.IsFalse(string.IsNullOrEmpty(cart.Id.ToString()));
        }

        [TestMethod]
        public void CreatingNewItem_ItemShouldBePopulatedByRandomValues()
        {
            ItemModel item = new ItemModel();
            
            Assert.IsFalse(string.IsNullOrEmpty(item.Id.ToString()));
            Assert.IsFalse(string.IsNullOrEmpty(item.Name));
            Assert.IsFalse(string.IsNullOrEmpty(item.Image));
            Assert.IsTrue(item.Price != 0);
            Assert.IsTrue(item.Quantity != 0);
        }
    }
}
