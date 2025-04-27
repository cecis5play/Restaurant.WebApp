using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Restaurant.WebApp.Data;
using Restaurant.WebApp.Data.Entities;
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
                Category = p.Category.Name,
                CategoryId = p.CategoryId,
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
               Price = p.Price,
               Description = p.Description,
               ImageUrl = p.ImageUrl
           })
               .FirstOrDefault();
            return product;
        }

        public int Create(ProductFormModel model)
        {
            var entity = new Product
            {
                Id = model.Id,
                Name = model.Name,
                Description = model.Description,
                Price = model.Price,
                Quantity = model.Quantity,
                ImageUrl = model.ImageUrl,
                CategoryId = model.CategoryId,

            };

            context.Products.Add(entity);
            context.SaveChanges();
            return entity.Id;
        }
        public List<CategoryModel> GetCategories()
        {
            return context.Categories
                 .Select(c => new CategoryModel
                 {
                     Id = c.Id,
                     Name = c.Name
                 }).ToList();
        }
        public void Edit(ProductFormModel model)
        {
            var product = this.context.Products.Where(v => v.Id == model.Id).First();

            product.Name = model.Name;
            product.Description = model.Description;
            product.Price = model.Price;
            product.Quantity = model.Quantity;
            product.ImageUrl = model.ImageUrl;
            product.CategoryId = model.CategoryId;

            this.context.SaveChanges();
        }
        public void EditCategory(CategoryModel category)
        {
            var product = this.context.Categories.Where(v => v.Id == category.Id).First();

            product.Name = category.Name;

            this.context.SaveChanges();
        }
        public bool Exists(int id)  => this.context.Products.Any(v => v.Id == id);
        public bool CategoryExists(int id) => this.context.Categories.Any(v => v.Id == id);

        public void Delete(int id)
        {
            var product = this.context.Products.First(h => h.Id == id);

            this.context.Products.Remove(product);
            this.context.SaveChanges();
        }
        public int CreateCategory(CategoryModel model)
        {
            var entity = new Category
            {
                Id = model.Id,
                Name = model.Name,
            };

            context.Categories.Add(entity);
            context.SaveChanges();
            return entity.Id;
        }
        public void DeleteCategory(int id)
        {
            var category = this.context.Categories.First(h => h.Id == id);

            this.context.Categories.Remove(category);
            this.context.SaveChanges();
        }
        public CategoryModel GetCategoryDetails(int id)
        { 
            var category = context.Categories.Where(p => p.Id == id)
           .Select(p => new CategoryModel
           {
               Id = id,
               Name = p.Name,
           })
               .First();
            return category;
        }

    }
}
