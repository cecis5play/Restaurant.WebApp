using System.ComponentModel.DataAnnotations;

namespace Restaurant.WebApp.Models
{
    public class DishReviewFormModel
    {
        [Required]
        public int ProductId { get; set; }

        [Required]
        [Range(1, 5)]
        public int Rating { get; set; }

        [MaxLength(1000)]
        public string? Comment { get; set; }
    }
}