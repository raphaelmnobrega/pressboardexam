
namespace pressboard.cashregisterexam.model
{
    /// <summary>
    /// Class Representing a Order Item
    /// </summary>
    public class OrderItem
    {
        /// <summary>
        /// Checking if item atribute is null, it is important once I am not
        /// implementing View-Models avoiding send a OrderItem with a Item null
        /// </summary>
        private Product item;
        public Product Item
        {
            get
            {
                if (item == null)
                    item = new Product();
                return item;
            }
            set
            {
                item = value;
            }
        }
        public double Quantity { get; set; }
    }
}
