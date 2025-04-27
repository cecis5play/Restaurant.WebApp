namespace Restaurant.WebApp.Models
{
    public class DishReviewViewModel
    {
        public int Id { get; set; }
        public int ProductId { get; set; }
        public int Rating { get; set; }
        public string? Comment { get; set; }
        public DateTime CreatedOn { get; set; }
        public string UserEmail { get; set; }
    }
}