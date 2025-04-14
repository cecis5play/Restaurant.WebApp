using Microsoft.AspNetCore.Mvc;
using Restaurant.WebApp.Data;
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
        public IActionResult OrderMenu(string searchString)
        {
            var products = service.GetAll();
            if (searchString != null)
            {
                products = products
                    .Where(p => p.Name.ToLower().Contains(searchString.ToLower()))
                    .ToList();
            }

            return View(products);
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
