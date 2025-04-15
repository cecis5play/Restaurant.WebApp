using Microsoft.AspNetCore.Mvc;
using Restaurant.WebApp.Data;
using Restaurant.WebApp.Data.Entities;
using Restaurant.WebApp.Models;
using Restaurant.WebApp.Services;

namespace Restaurant.WebApp.Controllers
{
    public class OrderController : Controller
    {
        private readonly IMenuService service;
        public readonly ApplicationDbContext context;


        public OrderController(IMenuService service, ApplicationDbContext context)
        {
            this.service = service;
            this.context = context;
        }
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult OrderMenu(int categoryId, string searchString)
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
            return View(product);
        }
    }
}
