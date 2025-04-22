using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Restaurant.WebApp.Data;
using Restaurant.WebApp.Models;
using Restaurant.WebApp.Services;

namespace Restaurant.WebApp.Controllers
{
    [Authorize(Roles = "Admin,Manager")]
    public class AdminController : Controller
    {
        private readonly IOrderService orderService;
        public readonly ApplicationDbContext context;
        private readonly UserManager<IdentityUser> userManager;
        public AdminController(IOrderService orderService, ApplicationDbContext context, UserManager<IdentityUser> userManager)
        {
            this.orderService = orderService;
            this.context = context;
            this.userManager = userManager;
        }

        public IActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public async Task<IActionResult> AllOrders()
        {
            var orders = orderService.GetAllOrders();
            var model = new List<OrderViewModel>();

            foreach (var order in orders)
            {
                var user = await userManager.FindByIdAsync(order.UserId);
                var customerName = user != null ? user.UserName : "Неизвестен клиент";

                model.Add(new OrderViewModel
                {
                    Id = order.Id,
                    OrderDate = order.OrderDate,
                    Status = order.Status,
                    TotalPrice = order.TotalPrice,
                    CustomerName = customerName,
                    OrderItems = order.OrderItems.Select(oi => new OrderItemViewModel
                    {
                        ProductName = oi.Product.Name,
                        Quantity = oi.Quantity,
                        Price = oi.Product.Price
                    }).ToList()
                });
            }

            return View(model);
        }
        [HttpPost]
        public IActionResult UpdateStatus(int orderId, string status)
        {
            var order = context.Orders.Find(orderId);
            if (order != null)
            {
                order.Status = status;
                context.SaveChanges();
                TempData["message"] = $"Статусът е сменен на {status}";
            }
            return RedirectToAction("AllOrders");
        }

        [HttpPost]
        public IActionResult Delete(int orderId)
        {
            orderService.DeleteOrder(orderId);
            return RedirectToAction("AllOrders");
        }
    }
}
