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
        [StringLength(50, ErrorMessage = "Коментарът трябва да е до 50 символа.")]
        public string? Comment { get; set; }
    }
}