using System.ComponentModel.DataAnnotations;

namespace Restaurant.WebApp.Models
{
    public class CategoryModel
    {


        public int Id { get; set; }
        public string Name { get; set; }
        /*  [Required]
          public int Id { get; set; }
          [Required]
          [MaxLength(15)]
          public string Name { get; set; }
          public string ImageUrl { get; set; }
          [Required]
          public DateTime CreatedDate { get; set; }
        */
    }
}
