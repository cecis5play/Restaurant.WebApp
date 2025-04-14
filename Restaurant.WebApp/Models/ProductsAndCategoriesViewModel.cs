using System.Collections.Generic;

namespace Restaurant.WebApp.Models
{
    public class ProductsAndCategoriesViewModel
    {
        public IEnumerable<ProductViewModel> Products  { get; set; }
        public IEnumerable<CategoryModel> Categories  { get; set; }
    }

}
