using System;

namespace pressboard.cashregisterexam.redis.Exceptions
{
    /// <summary>
    /// This Class is an example of Custom Exception. This exception should be used in
    /// OrderRepository's methods
    /// </summary>
    public class OrderRepositoryException : Exception
    {
        public OrderRepositoryException(string message)
        : base(message)
        {
        }
    }
}
