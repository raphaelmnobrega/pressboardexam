using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using pressboard.cashregisterexam.model.Services;
using System.Collections.Generic;
using pressboard.cashregisterexam.model;
using pressboard.cashregisterexam.redis.Repositories;

namespace pressboard.cashregisterexam.web.Tests
{
    [TestClass]
    public class BulkDiscountTest
    {
        private DiscountService ServiceDiscount
        {
            get
            {
                return new DiscountService(new DiscountRepository());
            }
        }

        private OrderService ServiceOrder
        {
            get
            {
                return new OrderService(new OrderRepository());
            }
        }

        private Order getOneOrderForTest()
        {

            Order order = new Order()
            {
                Key = "OrderBulkDiscount",
                Items = getListOfOrderItems()
            };

            return order;
        }

        private List<OrderItem> getListOfOrderItems()
        {
            Product product1 = new Product { Name = "Printer", Price = 65, Unit = ProductUnit.UNIT };
            Product product2 = new Product { Name = "Nutella", Price = 3.25, Unit = ProductUnit.UNIT, OnSale = true };
            Product product3 = new Product { Name = "Beef", Price = 15.50, Unit = ProductUnit.KG };
            Product product4 = new Product { Name = "CellPhone", Price = 119.90, Unit = ProductUnit.UNIT };

            OrderItem item1 = new OrderItem()
            {
                Item = product1,
                Quantity = 1
            };

            OrderItem item2 = new OrderItem()
            {
                Item = product2,
                Quantity = 4
            };

            OrderItem item3 = new OrderItem()
            {
                Item = product3,
                Quantity = 0.5
            };

            OrderItem item4 = new OrderItem()
            {
                Item = product4,
                Quantity = 1
            };

            var items = new List<OrderItem>()
            {
                item1, item2, item3, item4
            };

            return items;
        }

    
        [TestMethod]
        public void ApplyBulkDiscount()
        {
            var order = getOneOrderForTest();

            //Creating new order on Redis
            ServiceOrder.createOrder(order.Key, order);

            //Getting created order from Redis
            order = ServiceOrder.getOrder(order.Key);

            //Verifying Total Before Apply discount
            Assert.AreEqual(205.65, order.Total);

            //Applying Discount
            ServiceDiscount.ApplyBulkDiscount(order);

            //Getting order from Redis after apply discount
            order = ServiceOrder.getOrder(order.Key);

            //Verifing new Total with Discount 
            Assert.AreEqual(199.15, order.Total);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void NotApplyBulkDiscountWhenOrderNotExist()
        {
            var fakeOrder = new Order() { Key = "NotExist" };
            //Expecting Exception once this Key does not exist on Redis
            ServiceDiscount.ApplyBulkDiscount(fakeOrder);
        }

        [TestMethod]
        public void NotApplyBulkDiscountWhenItWasAlreadyApplied()
        {
            var fakeOrder = getOneOrderForTest();
            fakeOrder.Discount = 10;
            //Expecting return Null when discount is already applied
            Assert.IsNull(ServiceDiscount.ApplyBulkDiscount(fakeOrder));
        }
    }
}
