
namespace pressboard.cashregisterexam.model.Interfaces.Repository
{
    /// <summary>
    /// Interface that represents a Order Repository contract
    /// </summary>
    public interface IOrderRepository
    {
        Order getOrder(string key);
        void createOrder(string key, Order order);
    }
}
