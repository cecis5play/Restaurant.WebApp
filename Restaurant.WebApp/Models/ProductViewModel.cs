using Restaurant.WebApp.Data.Entities;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Restaurant.WebApp.Models
{
    public class ProductViewModel
    {
        public ProductViewModel()
        {
            Categories = new List<CategoryModel>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal? Price { get; set; }
        public int? Quantity { get; set; }
        public string ImageUrl { get; set; }
        public DateTime? CreatedDate { get; set; }
        public string Category { get; set; }
        public int CategoryId { get; set; }
        public IEnumerable<CategoryModel> Categories { get; set; }



        
    }
}
