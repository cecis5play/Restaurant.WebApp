namespace Restaurant.WebApp.Models
{
    public class OrderViewModel
    {
        public int Id { get; set; }
        public DateTime OrderDate { get; set; }
        public List<OrderItemViewModel> OrderItems { get; set; }
        public string? Status { get; set; }
        public string CustomerName { get; set; }
        public decimal? TotalPrice { get; set; }
        
    }
}
