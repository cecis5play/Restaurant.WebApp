using System.ComponentModel.DataAnnotations;

namespace Restaurant.WebApp.Models
{
    public class ProductFormModel
    {
        public ProductFormModel()
        {
            Categories = new List<CategoryModel>();
        }
        public int Id { get; set; }
        [Required(ErrorMessage = "Моля, въведете съобщение.")]
        [StringLength(1000, ErrorMessage = "Съобщението трябва да е до 1000 символа.")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Моля, въведете съобщение.")]
        [StringLength(1000, ErrorMessage = "Съобщението трябва да е до 1000 символа.")]
        public string Description { get; set; }
        [Required(ErrorMessage = "Моля, въведете Цена.")]
        [Range(typeof(decimal), "1", "1000", ErrorMessage = "Цената трябва да е от 1 лв до 1000 лв.")]
        public decimal? Price { get; set; }
        public int? Quantity { get; set; }
        [Required(ErrorMessage = "Моля, въведете линк на снимка.")]
        public string ImageUrl { get; set; } 
        [Required(ErrorMessage = "Моля, изберете категория.")]
        public int CategoryId { get; set; }
        public IEnumerable<CategoryModel> Categories { get; set; }
    }
}
