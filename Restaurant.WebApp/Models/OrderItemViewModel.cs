namespace Restaurant.WebApp.Models
{
    public class OrderItemViewModel
    {
        public string ProductName { get; set; }
        public int Quantity { get; set; }
        public decimal? Price { get; set; } 
        public decimal? Total => Price * Quantity; 
    }
}
