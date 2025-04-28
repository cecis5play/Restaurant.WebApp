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
        [Authorize(Roles = "Admin,Manager")]
        public IActionResult DeleteMessage(int id)
        {
            var message = context.ContactMessages.FirstOrDefault(m => m.Id == id);

            if (message == null || !message.IsHandled)
            {
                TempData["message"] = "Не може да изтриете необработено съобщение.";
                return RedirectToAction("All");
            }

            context.ContactMessages.Remove(message);
            context.SaveChanges();

            TempData["message"] = "Съобщението беше изтрито успешно.";
            return RedirectToAction("Index");
        }

        [HttpPost]
        [Authorize(Roles = "Admin,Manager")]
        public IActionResult MarkAsHandled(int id)
        {
            var message = context.ContactMessages.FirstOrDefault(m => m.Id == id);

            if (message != null)
            {
                message.IsHandled = true;
                context.SaveChanges();
                TempData["message"] = "Съобщението беше маркирано като обработено.";
            }

            return RedirectToAction("Index");
        }
    }
}