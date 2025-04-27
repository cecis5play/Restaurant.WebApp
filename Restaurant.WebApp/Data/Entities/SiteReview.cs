using System;
using System.ComponentModel.DataAnnotations;

namespace Restaurant.WebApp.Data.Entities
{
    public class SiteReview
    {
        public int Id { get; set; }

        [Required]
        public string UserId { get; set; }

        [Range(1, 5)]
        public int Rating { get; set; }

        [MaxLength(500)]
        public string Comment { get; set; }

        public DateTime CreatedOn { get; set; } = DateTime.UtcNow;
    }
}