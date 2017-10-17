
namespace pressboard.cashregisterexam.model.Interfaces.Services
{
    /// <summary>
    /// Interface that represents a Order Services contract
    /// </summary>
    public interface IOrderService
    {
        Order getOrder(string key);
        void createOrder(string key, Order order);
    }
}
