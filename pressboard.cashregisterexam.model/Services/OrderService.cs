using pressboard.cashregisterexam.model.Interfaces.Repository;
using pressboard.cashregisterexam.model.Interfaces.Services;

namespace pressboard.cashregisterexam.model.Services
{
    public class OrderService : IOrderService
    {
        private IOrderRepository repository;

        /// <summary>
        /// Contructor receiving a repository to apply dependency injection
        /// </summary>
        /// <param name="repository">Object of repository</param>
        public OrderService(IOrderRepository repository)
        {
            this.repository = repository;
        }

        /// <summary>
        /// Send a new order to repository
        /// </summary>
        /// <param name="key">Redis Key</param>
        /// <param name="order">Order to be save on Redis</param>
        public void createOrder(string key, Order order)
        {
            this.repository.createOrder(key, order);
        }

        /// <summary>
        /// Getting a order from repository
        /// </summary>
        /// <param name="key">Redis Key</param>
        /// <returns>Order retrieved from repository</returns>
        public Order getOrder(string key)
        {
            return this.repository.getOrder(key);
        }

    }
}
