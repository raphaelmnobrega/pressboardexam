using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using pressboard.cashregisterexam.model.Services;
using pressboard.cashregisterexam.redis.Repositories;
using pressboard.cashregisterexam.model;
using System.Collections.Generic;

namespace pressboard.cashregisterexam.web.Tests
{
    [TestClass]
    public class CouponDiscountTest
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

        [TestMethod]
        public void ApplyCouponDiscount()
        {
            var order = getOneOrderForTest();

            //Creating new order on Redis
            ServiceOrder.createOrder(order.Key, order);

            //Getting created order from Redis
            order = ServiceOrder.getOrder(order.Key);

            //Verifying Total without discount
            Assert.AreEqual(199.15, order.Total);

            //Applying Discount and Checking if the return is not null
            Assert.IsNotNull(ServiceDiscount.ApplyCouponDiscount(order.Key, 100, 5));

            //Getting order with discount applied from Redis
            order = ServiceOrder.getOrder(order.Key);

            //Verifing new Total with Discount 
            Assert.AreEqual(194.15, order.Total);
        }
      

        [TestMethod]
        public void NotApplyCouponDiscountWhenItWasAlreadyApplied()
        {
            var order = getOneOrderForTest();

            //Getting created order from Redis
            order = ServiceOrder.getOrder(order.Key);

            //Applying Discount and verifying if return is not null
            Assert.IsNull(ServiceDiscount.ApplyCouponDiscount(order.Key, 100, 20));

            //Getting order from Redis after try apply discount
            order = ServiceOrder.getOrder(order.Key);

            //Verifing new Total with Discount 
            Assert.AreEqual(194.15, order.Total);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void NotApplyCouponDiscountWhenOrderNotExist()
        {
            //Expecting Exception once this Key does not exist on Redis
            ServiceDiscount.ApplyCouponDiscount("NotExist", 100, 20);
        }


        private Order getOneOrderForTest()
        {

            Order order = new Order()
            {
                Key = "OrderCoupon",
                Items = getListOfOrderItems()
            };

            return order;
        }

        private List<OrderItem> getListOfOrderItems()
        {
            Product product1 = new Product { Name = "Printer", Price = 65, Unit = ProductUnit.UNIT };
            Product product2 = new Product { Name = "Nutellla", Price = 3.25, Unit = ProductUnit.UNIT };
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
                Quantity = 2
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
    }
}
