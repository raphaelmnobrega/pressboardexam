using System.Collections.Generic;
using System.Linq;

namespace pressboard.cashregisterexam.model
{
    public class Order
    {
        public string Key { get; set; }
        /// <summary>
        /// Checking if atribute items is null, it is important once I am not
        /// implementing View-Models avoiding send a Order with a List of Items null
        /// </summary>
        private List<OrderItem> items;
        public List<OrderItem> Items
        {
            get
            {
                if (items == null)
                    items = new List<OrderItem>();
                return items;
            }
            set
            {
                items = value;
            }
        }

        public double Discount { get; set; }

        /// <summary>
        /// The total is always the final total, in case of discount applied
        /// the total will be total less the discount.
        /// Is important to highlight that I am considering that is note possible
        /// have two discount in the same order
        /// </summary>
        public double Total
        {
            get
            {
                if (Items != null)
                    return (Items.Sum(i => i.Item.Price * i.Quantity)) - Discount;
                else
                    return 0;
            }
        }

    }
}
