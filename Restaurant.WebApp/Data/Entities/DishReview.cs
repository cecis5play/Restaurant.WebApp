
    using Microsoft.AspNetCore.Identity;
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    namespace Restaurant.WebApp.Data.Entities
    {
        public class DishReview
        {
            public int Id { get; set; }

            [Required]
            [Range(1, 5)]
            public int Rating { get; set; } // Оценка от 1 до 5

            [MaxLength(1000)]
            public string? Comment { get; set; } // Коментар (по желание)

            [Required]
            public DateTime CreatedOn { get; set; } = DateTime.UtcNow;

            [Required]
            public int ProductId { get; set; }

            [ForeignKey(nameof(ProductId))]
            public Product Product { get; set; }

            [Required]
            public string UserId { get; set; }

            [ForeignKey(nameof(UserId))]
            public IdentityUser User { get; set; }
        }
    }
