using pressboard.cashregisterexam.model.Interfaces.Repository;
using System;
using StackExchange.Redis;
using pressboard.cashregisterexam.redis.Properties;
using Newtonsoft.Json;

namespace pressboard.cashregisterexam.redis.Repositories
{
    public class DiscountRepository : IDiscountRepository
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
        /// Applying Bulk Discount In a order and saving on Redis Cache
        /// </summary>
        /// <param name="orderKey">Redis Key</param>
        /// <param name="discountValue">Value to be discounted</param>
        /// <returns></returns>
        public model.Order ApplyBulkDiscount(string orderKey, double discountValue)
        {
            var deserializedObj = JsonConvert.DeserializeObject<model.Order>(Cache.StringGet(orderKey));
            if (deserializedObj.Discount == 0)
            { 
                deserializedObj.Discount = discountValue;
                Cache.StringSet(deserializedObj.Key, JsonConvert.SerializeObject(deserializedObj));
                return deserializedObj;
            }
            return null;
        }

        /// <summary>
        /// Applying coupon discount in a order and saving on Redis Cache
        /// </summary>
        /// <param name="orderKey">Redis Key</param>
        /// <param name="threshold">Value that represents a threshold</param>
        /// <param name="discountValue">Value of discount to be applied</param>
        /// <returns></returns>
        public model.Order ApplyCouponDiscount(string orderKey, double threshold, double discountValue)
        {
            var deserializedObj = JsonConvert.DeserializeObject<model.Order>(Cache.StringGet(orderKey));
            if (deserializedObj.Discount == 0 && deserializedObj.Total > threshold)
            {
                deserializedObj.Discount = discountValue;
                Cache.StringSet(deserializedObj.Key, JsonConvert.SerializeObject(deserializedObj));
                return deserializedObj;
            }
            return null;
        }
    }
}
