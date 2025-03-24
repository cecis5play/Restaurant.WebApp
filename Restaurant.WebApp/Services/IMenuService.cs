using Restaurant.WebApp.Models;

namespace Restaurant.WebApp.Services
{
    public interface IMenuService
    {
        public IEnumerable<ProductViewModel> GetAll();

        public ProductDetailViewModel GetProductDetails(int id);
    }
}
