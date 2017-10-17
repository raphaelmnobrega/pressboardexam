
namespace pressboard.cashregisterexam.model.Interfaces.Repository
{
    /// <summary>
    /// Interface that represents a Discount Repository contract
    /// </summary>
    public interface IDiscountRepository
    {
        Order ApplyBulkDiscount(string orderKey, double discountValue);
        Order ApplyCouponDiscount(string orderKey, double threshold, double discountValue);
    }
}
