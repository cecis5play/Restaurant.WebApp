using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Restaurant.WebApp.Data;

namespace Restaurant.WebApp.Controllers
{
    [Authorize(Roles = "Admin,Manager")]
    public class AdminContactController : Controller
    {
        private readonly ApplicationDbContext context;

        public AdminContactController(ApplicationDbContext context)
        {
            this.context = context;
        }

        public IActionResult Index()
        {
            var messages = context.ContactMessages
                .OrderByDescending(m => m.CreatedOn)
                .ToList();

            return View(messages);
        }

        [HttpPost]
        public async Task<IActionResult> MarkAsHandled(int id)
        {
            var message = await context.ContactMessages.FindAsync(id);

            if (message != null)
            {
                message.IsHandled = true;
                await context.SaveChangesAsync();
                TempData["Success"] = "Съобщението е маркирано като обработено.";
            }

            return RedirectToAction("Index");
        }
    }
}