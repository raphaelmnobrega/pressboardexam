using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using pressboard.cashregisterexam.model;
using pressboard.cashregisterexam.model.Services;
using pressboard.cashregisterexam.redis.Repositories;
using pressboard.cashregisterexam.redis.Exceptions;

namespace pressboard.cashregisterexam.web.Tests
{
    [TestClass]
    public class OrderTest
    {
        private OrderService Service
        {
            get
            {
                return new OrderService(new OrderRepository());
            }
        }

        [TestMethod]
        public void CheckIfOrderWasCreateOnRedis()
        {
            Service.createOrder("OrderTest1", getOneOrderForTest());
            
            var order = Service.getOrder("OrderTest1");

            Assert.IsNotNull(order);
            Assert.AreEqual(4, order.Items.Count);
        }

        [TestMethod]
        public void CheckRetrievingOrderAlreadyOnRedis()
        {
            var order = Service.getOrder("OrderTest1");

            Assert.IsNotNull(order);
        }

        [TestMethod]
        public void CheckTotal()
        {
            var order = Service.getOrder("OrderTest1");

            Assert.AreEqual(17.6875,order.Total);
        }

        [TestMethod]
        [ExpectedException(typeof(OrderRepositoryException))]
        public void CheckIfOrderDoesNotExit()
        {
            var order = Service.getOrder("OrderTest2");
        }

        private Order getOneOrderForTest()
        {
          
            Order order = new Order()
            {
                Items = getListOfOrderItems()
            };

            return order;
        }

        private List<OrderItem> getListOfOrderItems()
        {
            Product product1 = new Product { Name = "Canada Dry Box", Price = 5.90, Unit = ProductUnit.BOX };
            Product product2 = new Product { Name = "Nutellla", Price = 3.25, Unit = ProductUnit.UNIT };
            Product product3 = new Product { Name = "Onin", Price = 1.25, Unit = ProductUnit.KG };
            Product product4 = new Product { Name = "OhHenry", Price = 1.50, Unit = ProductUnit.UNIT };

            OrderItem item1 = new OrderItem()
            {
                Item = product1,
                Quantity = 1
            };

            OrderItem item2 = new OrderItem()
            {
                Item = product2,
                Quantity = 2
            };

            OrderItem item3 = new OrderItem()
            {
                Item = product3,
                Quantity = 0.63
            };

            OrderItem item4 = new OrderItem()
            {
                Item = product4,
                Quantity = 3
            };

            var items = new List<OrderItem>()
            {
                item1, item2, item3, item4
            };

            return items;
        }
    }
}
