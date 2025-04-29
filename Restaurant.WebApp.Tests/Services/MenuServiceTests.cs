using NUnit.Framework;
using Microsoft.EntityFrameworkCore;
using Restaurant.WebApp.Data;
using Restaurant.WebApp.Services;
using Restaurant.WebApp.Models;
using Restaurant.WebApp.Data.Entities;
using System.Linq;

namespace Restaurant.WebApp.Tests.Services
{
    public class MenuServiceTests
    {
        private ApplicationDbContext context;
        private MenuService menuService;

        [SetUp]
        public void Setup()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: "TestDatabase_" + System.Guid.NewGuid().ToString())
                .Options;

            context = new ApplicationDbContext(options);
            menuService = new MenuService(context);
        }

        [TearDown]
        public void TearDown()
        {
            context.Database.EnsureDeleted();
            context.Dispose();
        }

        [Test]
        public void GetAll_ShouldReturnAllProducts()
        {
            // Arrange
            context.Products.Add(new Product { Name = "Pizza", Price = 10, Description = "Delicious", Category = new Category { Name = "Main" }, ImageUrl = "ImageUrl" });
            context.Products.Add(new Product { Name = "Burger", Price = 8, Description = "Tasty", Category = new Category { Name = "Fast Food" }, ImageUrl = "ImageUrl" });
            context.SaveChanges();

            // Act
            var products = menuService.GetAll();

            // Assert
            Assert.AreEqual(2, products.Count());
        }

        [Test]
        public void Create_ShouldAddProduct()
        {
            // Arrange
            var category = new Category { Name = "Test" };
            context.Categories.Add(category);
            context.SaveChanges();

            var model = new ProductFormModel
            {
                Name = "Pasta",
                Description = "Italian style",
                Price = 12,
                ImageUrl = "http://image.com/pasta.jpg",
                CategoryId = category.Id
            };

            // Act
            var productId = menuService.Create(model);

            // Assert
            var createdProduct = context.Products.FirstOrDefault(p => p.Id == productId);
            Assert.IsNotNull(createdProduct);
            Assert.AreEqual("Pasta", createdProduct.Name);
        }

        [Test]
        public void GetProductDetails_ShouldReturnCorrectProduct()
        {
            // Arrange
            var product = new Product
            {
                Name = "Steak",
                Description = "Grilled beef",
                Price = 25,
                ImageUrl = "http://image.com/steak.jpg",
                Category = new Category { Name = "Grill" }
            };
            context.Products.Add(product);
            context.SaveChanges();

            // Act
            var productDetails = menuService.GetProductDetails(product.Id);

            // Assert
            Assert.IsNotNull(productDetails);
            Assert.AreEqual("Steak", productDetails.Name);
            Assert.AreEqual(25, productDetails.Price);
        }

        [Test]
        public void Exists_ShouldReturnTrue_IfProductExists()
        {
            // Arrange
            var product = new Product { Name = "Sushi", Price = 20, Category = new Category { Name = "Japanese" }, Description = "Description" , ImageUrl = "ImageUrl" };
            context.Products.Add(product);
            context.SaveChanges();

            // Act
            var exists = menuService.Exists(product.Id);

            // Assert
            Assert.IsTrue(exists);
        }

        [Test]
        public void Exists_ShouldReturnFalse_IfProductDoesNotExist()
        {
            // Act
            var exists = menuService.Exists(999);

            // Assert
            Assert.IsFalse(exists);
        }

        [Test]
        public void Edit_ShouldUpdateProduct()
        {
            // Arrange
            var product = new Product
            {
                Name = "Soup",
                Description = "Hot",
                Price = 5,
                ImageUrl = "http://image.com/soup.jpg",
                Category = new Category { Name = "Soups" }
            };
            context.Products.Add(product);
            context.SaveChanges();

            var model = new ProductFormModel
            {
                Id = product.Id,
                Name = "Updated Soup",
                Description = "Very hot",
                Price = 6,
                ImageUrl = "http://image.com/soup-new.jpg",
                CategoryId = product.CategoryId
            };

            // Act
            menuService.Edit(model);

            // Assert
            var updatedProduct = context.Products.Find(product.Id);
            Assert.AreEqual("Updated Soup", updatedProduct.Name);
            Assert.AreEqual(6, updatedProduct.Price);
        }

        [Test]
        public void Delete_ShouldRemoveProduct()
        {
            // Arrange
            var product = new Product
            {
                Name = "Cake",
                Price = 4,
                Category = new Category { Name = "Desserts" },
                Description = "Top cake",
                ImageUrl = "...."
            };
            context.Products.Add(product);
            context.SaveChanges();

            // Act
            menuService.Delete(product.Id);

            // Assert
            var deletedProduct = context.Products.Find(product.Id);
            Assert.IsNull(deletedProduct);
        }

        [Test]
        public void GetCategories_ShouldReturnAllCategories()
        {
            // Arrange
            context.Categories.Add(new Category { Name = "Main" });
            context.Categories.Add(new Category { Name = "Dessert" });
            context.SaveChanges();

            // Act
            var categories = menuService.GetCategories();

            // Assert
            Assert.AreEqual(2, categories.Count());
        }

        [Test]
        public void CreateCategory_ShouldAddCategory()
        {
            // Arrange
            var model = new CategoryModel
            {
                Name = "Grill"
            };

            // Act
            var categoryId = menuService.CreateCategory(model);

            // Assert
            var createdCategory = context.Categories.Find(categoryId);
            Assert.IsNotNull(createdCategory);
            Assert.AreEqual("Grill", createdCategory.Name);
        }

        [Test]
        public void EditCategory_ShouldUpdateCategory()
        {
            // Arrange
            var category = new Category { Name = "Old Name" };
            context.Categories.Add(category);
            context.SaveChanges();

            var model = new CategoryModel
            {
                Id = category.Id,
                Name = "New Name"
            };

            // Act
            menuService.EditCategory(model);

            // Assert
            var updatedCategory = context.Categories.Find(category.Id);
            Assert.AreEqual("New Name", updatedCategory.Name);
        }

        [Test]
        public void DeleteCategory_ShouldRemoveCategory()
        {
            // Arrange
            var category = new Category { Name = "Delete Me" };
            context.Categories.Add(category);
            context.SaveChanges();

            // Act
            menuService.DeleteCategory(category.Id);

            // Assert
            var deletedCategory = context.Categories.Find(category.Id);
            Assert.IsNull(deletedCategory);
        }

        [Test]
        public void GetCategoryDetails_ShouldReturnCorrectCategory()
        {
            // Arrange
            var category = new Category { Name = "Test Category" };
            context.Categories.Add(category);
            context.SaveChanges();

            // Act
            var result = menuService.GetCategoryDetails(category.Id);

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual("Test Category", result.Name);
        }

        [Test]
        public void CategoryExists_ShouldReturnTrueIfCategoryExists()
        {
            // Arrange
            var category = new Category { Name = "Appetizers" };
            context.Categories.Add(category);
            context.SaveChanges();

            // Act
            var exists = menuService.CategoryExists(category.Id);

            // Assert
            Assert.IsTrue(exists);
        }

        [Test]
        public void CategoryExists_ShouldReturnFalseIfCategoryDoesNotExist()
        {
            // Act
            var exists = menuService.CategoryExists(999);

            // Assert
            Assert.IsFalse(exists);
        }
    }
}