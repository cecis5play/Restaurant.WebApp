using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Restaurant.WebApp.Controllers
{
    [Authorize(Roles = "Admin,Manager")]
    public class AdminPanelController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}