using pressboard.cashregisterexam.model.Interfaces.Repository;
using pressboard.cashregisterexam.model.Interfaces.Services;
using System;

namespace pressboard.cashregisterexam.model.Services
{
    /// <summary>
    /// Discount services class reponsible for Discounts functions
    /// </summary>
    public class DiscountService : IDiscountService
    {
        private IDiscountRepository repository;
        /// <summary>
        /// Contructor receiving a repository to apply dependency injection
        /// </summary>
        /// <param name="repository">Object of repository</param>
        public DiscountService(IDiscountRepository repository)
        {
            this.repository = repository;
        }

        /// <summary>
        /// Apply bulk discount and call repository in order to save discount on Redis
        /// </summary>
        /// <param name="currentOrder">Current Order</param>
        /// <returns>A order with bulk discount applied</returns>
        public Order ApplyBulkDiscount(Order currentOrder)
        {
            return this.repository.ApplyBulkDiscount(currentOrder.Key, getBulkDiscount(currentOrder));
        }

        /// <summary>
        /// Apply coupon discount and call repository in order to save discount on Redis
        /// </summary>
        /// <param name="orderKey">Redis Key</param>
        /// <param name="threshold">Value that represents a threshold</param>
        /// <param name="discountValue">Value to be discounted</param>
        /// <returns>A order with bulk discount applied</returns>
        public Order ApplyCouponDiscount(string orderKey, double threshold, double discountValue)
        {
            return this.repository.ApplyCouponDiscount(orderKey,threshold,discountValue);
        }

        /// <summary>
        /// Method where the bulk discount is applied.
        /// A item must be OnSale and is not applied for item with unit KG
        /// </summary>
        /// <param name="currentOrder"></param>
        /// <returns>Total with discount</returns>
        private double getBulkDiscount(Order currentOrder)
        {
            double totalDiscount = 0;
            foreach (var currentItem in currentOrder.Items)
            {
                if (currentItem.Item.OnSale && (currentItem.Item.Unit == ProductUnit.BOX || currentItem.Item.Unit == ProductUnit.UNIT))
                {
                    int quantityToApplyDiscount = (int)(currentItem.Quantity / 2);
                    double discountItemValue = currentItem.Item.Price * quantityToApplyDiscount;
                    totalDiscount += discountItemValue;
                }
            }
            return totalDiscount;
        }
    }
}
