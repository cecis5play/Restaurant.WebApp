using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Restaurant.WebApp.Data;
using Restaurant.WebApp.Data.Entities;
using Restaurant.WebApp.Models;
using Restaurant.WebApp.Services;

namespace Restaurant.WebApp.Controllers
{
    public class MenuController : Controller
    {
        private readonly IMenuService service;
        public readonly ApplicationDbContext context;


        public MenuController(IMenuService service, ApplicationDbContext context)
        {
            this.service = service;
            this.context = context;
        }
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult All(int categoryId, string searchString)
        {
            ProductsAndCategoriesViewModel model = new ProductsAndCategoriesViewModel();
            model.Products = service.GetAll();
            model.Categories = service.GetCategories();
            //var products = service.GetAll();
            if (searchString != null)
            {
                model.Products = model.Products
                    .Where(p => p.Name.ToLower().Contains(searchString.ToLower()))
                    .ToList();
            }

            model.Products = model.Products
                .Where(c => c.CategoryId == categoryId)
                .ToList();



            return View(model);
        }
        public IActionResult ProductDetails(int id)
        {
            var product = service.GetProductDetails(id);

            if (product == null)
            {
                return BadRequest();
            }
            return View(product);
        }

        [HttpGet]
        [Authorize]
        public IActionResult Add()
        {
            var categories = service.GetCategories();
            var model = new ProductFormModel()
            {
                Categories = categories
            };


            return View(model);
        }
        private List<CategoryModel> GetCategories()
        {
            return context.Categories
                .Select(c => new CategoryModel
                {
                    Id = c.Id,
                    Name = c.Name
                }).ToList();
        }
        [HttpPost]
        public IActionResult Add(ProductFormModel model)
        {

            if (!ModelState.IsValid)
            {
                return View(model);
            }
            var product = service.Create(model);
            return RedirectToAction(nameof(ProductDetails), new { Id = product });
        }
        public IActionResult Edit(int id)
        {
            var product = this.service.GetProductDetails(id);

            if (!this.service.Exists(id))
            {
                return BadRequest();
            }

            var Categories = service.GetCategories();
            var productModel = new ProductFormModel()
            {
                Id = product.Id,
                Name = product.Name,
                Description = product.Description,
                Price = product.Price,
                Quantity = product.Quantity,
                ImageUrl = product.ImageUrl,
                Categories = Categories,
                
            };

            return View(productModel);
        }
        [HttpPost]
        public IActionResult Edit(ProductFormModel model, int id)
        {
            if (!this.service.Exists(id))
            {
                return BadRequest();
            }
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            this.service.Edit(model);

            return RedirectToAction(nameof(ProductDetails), new { id = id });


        }
        [HttpGet]
        public IActionResult Delete(int id)
        {

            if (!this.service.Exists(id))
            {
                return BadRequest();
            }

            var product = this.service.GetProductDetails(id);

            var model = new ProductDetailViewModel()
            {
                Id = product.Id,
                Name = product.Name,
                Description = product.Description,
                ImageUrl = product.ImageUrl
            };
            return View(model);
        }
        [HttpPost]
        public IActionResult Delete(ProductDetailViewModel model)
        {
            if (!this.service.Exists(model.Id))
            {
                return BadRequest();
            }

            this.service.Delete(model.Id);

            return RedirectToAction(nameof(All));
        }
    }
}
    
