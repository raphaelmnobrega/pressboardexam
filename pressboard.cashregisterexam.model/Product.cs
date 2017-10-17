
namespace pressboard.cashregisterexam.model
{
    /// <summary>
    /// Class representing a Product
    /// </summary>
    public class Product
    {
        public string Name { get; set; }
        public double Price { get; set; }
        public ProductUnit Unit { get; set; }
        public bool OnSale { get; set; }
    }
}
