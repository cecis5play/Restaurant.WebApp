using Restaurant.WebApp.Data;
using Restaurant.WebApp.Models;

namespace Restaurant.WebApp.Services
{
    public class MenuService : IMenuService
    {
        public readonly ApplicationDbContext context;
        public MenuService(ApplicationDbContext context)
        {
            this.context = context;
        }
        public IEnumerable<ProductViewModel> GetAll()
        {
            var products = context.Products.Select(p => new ProductViewModel
            {
                Id = p.Id,
                Name = p.Name,
                Description = p.Description,
                Price = p.Price,
                Quantity = p.Quantity,
                ImageUrl = p.ImageUrl,
                Category = p.Category.Name
            }).ToList();

            return products;
        }
        public ProductDetailViewModel GetProductDetails(int id)
        {
            var product = context.Products.Where(p => p.Id == id)
           .Select(p => new ProductDetailViewModel
           {
               Id = id,
               Name = p.Name,
               Description = p.Description,
               ImageUrl = p.ImageUrl
           })
               .FirstOrDefault();
            return product;
        }


    }
}
