using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Restaurant.WebApp.Models;
using Restaurant.WebApp.Services;
using System.Diagnostics;

namespace Restaurant.WebApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ISiteReviewService siteReviewService;
        private readonly UserManager<IdentityUser> userManager;

        public HomeController(ILogger<HomeController> logger, ISiteReviewService siteReviewService, UserManager<IdentityUser> userManager)
        {
            _logger = logger;
            this.siteReviewService = siteReviewService;
            this.userManager = userManager;
        }

        public async Task<IActionResult> Index()
        {
            var reviews = await siteReviewService.GetAllReviewsAsync();
            return View(reviews);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
        [HttpPost]
        public async Task<IActionResult> AddSiteReview(int rating, string comment)
        {
            var userId = userManager.GetUserId(User);

            if (await siteReviewService.HasUserReviewedAsync(userId))
            {
                TempData["Error"] = "Вече сте оставили ревю за сайта!";
                return RedirectToAction("Index");
            }

            await siteReviewService.AddReviewAsync(userId, rating, comment);
            TempData["message"] = "Благодарим за вашето ревю!";
            return RedirectToAction("Index");
        }
        [HttpPost]
        [Authorize(Roles = "Admin,Manager")]
        public async Task<IActionResult> DeleteSiteReview(int reviewId)
        {
            await siteReviewService.DeleteSiteReviewAsync(reviewId);

            TempData["message"] = "Ревюто беше изтрито успешно!";
            return RedirectToAction("Index");
        }
    }
}
