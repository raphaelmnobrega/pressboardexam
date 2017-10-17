
namespace pressboard.cashregisterexam.model.Interfaces.Services
{
    /// <summary>
    /// Interface that represents a Discount Services contract
    /// </summary>
    public interface IDiscountService
    {
        Order ApplyBulkDiscount(Order currentOrder);
        Order ApplyCouponDiscount(string orderKey, double threshold, double discountValue);
    }
}
