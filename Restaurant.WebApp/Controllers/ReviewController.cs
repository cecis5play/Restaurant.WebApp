using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Restaurant.WebApp.Models;
using Restaurant.WebApp.Services;

namespace Restaurant.WebApp.Controllers
{
    [Authorize]
    public class ReviewController : Controller
    {
        private readonly IReviewService reviewService;
        private readonly UserManager<IdentityUser> userManager;

        public ReviewController(IReviewService reviewService, UserManager<IdentityUser> userManager)
        {
            this.reviewService = reviewService;
            this.userManager = userManager;
        }

        [HttpPost]
        public async Task<IActionResult> AddReview(DishReviewFormModel model)
        {
            var userId = userManager.GetUserId(User);

            if (await reviewService.HasUserReviewedAsync(model.ProductId, userId))
            {
                TempData["Error"] = "Вече сте оставили ревю за това ястие.";
                return RedirectToAction("ProductDetails", "Menu", new { id = model.ProductId });
            }

            if (!ModelState.IsValid)
            {
                TempData["Error"] = "Моля попълнете всички полета правилно.";
                return RedirectToAction("ProductDetails", "Menu", new { id = model.ProductId });
            }

            await reviewService.AddReviewAsync(model, userId);

            TempData["message"] = "Успешно добавено ревю!";

            return RedirectToAction("ProductDetails", "Menu", new { id = model.ProductId });
        }
        [HttpPost]
        [Authorize(Roles = "Admin,Manager")]
        public async Task<IActionResult> DeleteReview(int reviewId, int productId)
        {
            await reviewService.DeleteReviewAsync(reviewId);

            TempData["message"] = "Ревюто беше изтрито успешно!";
            return RedirectToAction("ProductDetails", "Menu", new { id = productId });
        }
    }
}