using Restaurant.WebApp.Models;

namespace Restaurant.WebApp.Services
{
    public interface IMenuService
    {
        public IEnumerable<ProductViewModel> GetAll();

        public ProductDetailViewModel GetProductDetails(int id);
        public void Edit(ProductFormModel model);
        public void EditCategory(CategoryModel model);
        public int Create(ProductFormModel model);
        public CategoryModel GetCategoryDetails(int id);
        public int CreateCategory(CategoryModel model);
        public void DeleteCategory(int id);
        public List<CategoryModel> GetCategories();
        bool Exists(int id);
        public bool CategoryExists(int id);
        public void Delete(int id);

    }
}
