using System.ComponentModel.DataAnnotations;

namespace Restaurant.WebApp.Models
{
    public class CategoryModel
    {


        public int Id { get; set; }
        [Required(ErrorMessage = "Моля, въведете име на категорията.")]
        [StringLength(20, ErrorMessage = "Съобщението трябва да е до 20 символа.")]
        public string Name { get; set; }
    }
}
