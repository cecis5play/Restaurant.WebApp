using System.ComponentModel.DataAnnotations;

namespace Restaurant.WebApp.Data.Entities
{
    public class Category
    {
        public Category()
        {
            Products = new HashSet<Product>();
        }
        [Required]
        [Key]
        public int Id { get; set; }
        [Required]
        [MaxLength(15)]
        public string Name { get; set; }

        public HashSet<Product> Products { get; set; }
    }
}
