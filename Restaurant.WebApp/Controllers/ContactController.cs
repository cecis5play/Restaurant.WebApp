using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Restaurant.WebApp.Data;
using Restaurant.WebApp.Data.Entities;
using Restaurant.WebApp.Models;

namespace Restaurant.WebApp.Controllers
{
    public class ContactController : Controller
    {
        private readonly ApplicationDbContext context;
        private readonly UserManager<IdentityUser> userManager;

        public ContactController(ApplicationDbContext context, UserManager<IdentityUser> userManager)
        {
            this.context = context;
            this.userManager = userManager;
        }

        [HttpGet]
        public IActionResult Index()
        {
            return View(new ContactViewModel());
        }

        [HttpPost]
        public async Task<IActionResult> Send(ContactViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View("Index", model);
            }

            var message = new ContactMessage
            {
                Email = model.Email,
                Message = model.Message,
                CreatedOn = DateTime.UtcNow
            };

            context.ContactMessages.Add(message);
            await context.SaveChangesAsync();

            TempData["message"] = "Вашето съобщение беше изпратено успешно! Благодарим ви.";
            return RedirectToAction("Index", "Home");
        }
    }
}