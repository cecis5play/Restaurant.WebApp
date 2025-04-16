using Microsoft.AspNetCore.Identity;
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
        private readonly IOrderService orderService;
        private readonly UserManager<IdentityUser> userManager;

        public readonly ApplicationDbContext context;


        public OrderController(IMenuService service, ApplicationDbContext context, IOrderService orderService, UserManager<IdentityUser> userManager)
        {
            this.service = service;
            this.context = context;
            this.orderService = orderService;
            this.userManager = userManager;
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
        [HttpGet]
        public IActionResult Cart()
        {
            var userId = userManager.GetUserId(User);
            var cartItems = orderService.GetCartItems(userId);

            var model = cartItems.Select(ci => new CartItemViewModel
            {
                Id = ci.Id,
                ProductName = ci.Product.Name,
                Price = ci.Product.Price,
                Quantity = ci.Quantity
            }).ToList();

            return View(model);
        }

        [HttpPost]
        public IActionResult AddToCart(int productId)
        {
            var userId = userManager.GetUserId(User);

            orderService.AddToCart(productId, userId);
            TempData["message"] = "Добавен продукт в количката";
            return RedirectToAction("OrderMenu");
        }

        [HttpPost]
        public IActionResult PlaceOrder()
        {
            var userId = userManager.GetUserId(User);
            orderService.PlaceOrder(userId);
            return RedirectToAction("OrderConfirmation");
        }

        [HttpGet]
        public IActionResult OrderConfirmation()
        {

            var userId = userManager.GetUserId(User);
            var lastOrder = orderService.GetLastOrder(userId); 

            var model = new OrderViewModel
            {
                Id = lastOrder.Id,
                OrderDate = lastOrder.OrderDate,
                OrderItems = lastOrder.OrderItems.Select(oi => new OrderItemViewModel
                {
                    ProductName = oi.Product.Name, 
                    Quantity = oi.Quantity
                }).ToList()
            };

            return View(model);
        }
        [HttpPost]
        public IActionResult IncreaseQuantity(int cartItemId)
        {
            orderService.IncreaseQuantity(cartItemId);
            return RedirectToAction("Cart");
        }

        [HttpPost]
        public IActionResult DecreaseQuantity(int cartItemId)
        {
            orderService.DecreaseQuantity(cartItemId);
            return RedirectToAction("Cart");
        }

        [HttpPost]
        public IActionResult RemoveFromCart(int cartItemId)
        {
            orderService.RemoveFromCart(cartItemId);
            return RedirectToAction("Cart");
        }
    }
}
