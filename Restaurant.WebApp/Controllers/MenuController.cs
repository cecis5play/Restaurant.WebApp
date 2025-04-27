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
        private readonly IReviewService reviewService;
        public readonly ApplicationDbContext context;


        public MenuController(IMenuService service, ApplicationDbContext context, IReviewService reviewService)
        {
            this.service = service;
            this.context = context;
            this.reviewService = reviewService;
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
            if (categoryId != 0)
            {
                model.Products = model.Products
                    .Where(c => c.CategoryId == categoryId)
                    .ToList();
            }



            return View(model);
        }
        public IActionResult ProductDetails(int id)
        {
            var product = service.GetProductDetails(id); 

            if (product == null)
            {
                return BadRequest();
            }

            var reviews = reviewService.GetReviewsByProductIdAsync(id).Result; 

            var model = new ProductDetailViewModel
            {
                Id = product.Id,
                Name = product.Name,
                Description = product.Description,
                Price = product.Price,
                ImageUrl = product.ImageUrl,
                Reviews = reviews 
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
        [HttpGet]
        [Authorize]
        public IActionResult AddCategory()
        {

            var model = new CategoryModel();
            return View(model);
        }
        [HttpPost]
        public IActionResult AddCategory(CategoryModel model)
        {

            if (!ModelState.IsValid)
            {
                return View(model);
            }
            var category = service.CreateCategory(model);
            return RedirectToAction(nameof(All), new { categoryId = category });
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
        public IActionResult EditCategory(int id)
        {
            var category = this.service.GetCategoryDetails(id);

            if (!this.service.CategoryExists(id))
            {
                return BadRequest();
            }

            var categoryModel = new CategoryModel()
            {
                Id = category.Id,
                Name = category.Name,

            };

            return View(categoryModel);
        }
        [HttpPost]
        public IActionResult EditCategory(CategoryModel model, int id)
        {
            if (!this.service.Exists(id))
            {
                return BadRequest();
            }
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            this.service.EditCategory(model);

            return RedirectToAction(nameof(All));


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
        [HttpGet]
        public IActionResult DeleteCategory(int id)
        {

            if (!this.service.CategoryExists(id))
            {
                return BadRequest();
            }

            var category = this.service.GetCategoryDetails(id);

            var model = new CategoryModel()
            {
                Id = category.Id,
                Name = category.Name,
            };
            return View(model);
        }
        [HttpPost]
        public IActionResult DeleteCategory(CategoryModel model)
        {
            if (!this.service.CategoryExists(model.Id))
            {
                return BadRequest();
            }

            this.service.DeleteCategory(model.Id);

            return RedirectToAction(nameof(All));
        }
    }
}
    
