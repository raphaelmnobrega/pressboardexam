using pressboard.cashregisterexam.model.Interfaces.Repository;
using System;
using pressboard.cashregisterexam.model;
using StackExchange.Redis;
using Newtonsoft.Json;
using pressboard.cashregisterexam.redis.Exceptions;
using pressboard.cashregisterexam.redis.Properties;

namespace pressboard.cashregisterexam.redis.Repositories
{
    public class OrderRepository : IOrderRepository
    {
        /// <summary>
        /// Getting Redis Connection in a Lazy Mode
        /// Connection string is getting from Resources
        /// </summary>
        private static Lazy<ConnectionMultiplexer> LazyConnection = new Lazy<ConnectionMultiplexer>(() => {
            return ConnectionMultiplexer.Connect(Resources.ConnectionString);
        });

        /// <summary>
        /// Getting Redis Cache 
        /// </summary>
        public IDatabase Cache
        {
            get
            {
                return LazyConnection.Value.GetDatabase();
            }
        }

        /// <summary>
        /// Creating a New Order and saving on redis
        /// </summary>
        /// <param name="key">Redis Key</param>
        /// <param name="order">Order to be saved</param>
        public void createOrder(string key, model.Order order)
        {
            var serializedObj = JsonConvert.SerializeObject(order);
            Cache.StringSet(key, serializedObj);
        }

        /// <summary>
        /// Retrieving an order cached on Redis
        /// </summary>
        /// <param name="key">Redis key</param>
        /// <returns></returns>
        public model.Order getOrder(string key) 
        {
            try
            {
                return JsonConvert.DeserializeObject<model.Order>(Cache.StringGet(key));
            }
            catch
            {
                throw new OrderRepositoryException("Order Not Found");
            }        
        }
    }
}
