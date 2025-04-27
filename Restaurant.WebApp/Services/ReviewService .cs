using Microsoft.EntityFrameworkCore;
using Restaurant.WebApp.Data;
using Restaurant.WebApp.Data.Entities;
using Restaurant.WebApp.Models;

namespace Restaurant.WebApp.Services
{
    public class ReviewService : IReviewService
    {
        private readonly ApplicationDbContext context;

        public ReviewService(ApplicationDbContext context)
        {
            this.context = context;
        }

        public async Task<IEnumerable<DishReviewViewModel>> GetReviewsByProductIdAsync(int productId)
        {
            return await context.DishReviews
                .Where(r => r.ProductId == productId)
                .Select(r => new DishReviewViewModel
                {
                    Id = r.Id,
                    Rating = r.Rating,
                    Comment = r.Comment,
                    CreatedOn = r.CreatedOn,
                    ProductId = r.ProductId,
                    UserEmail = r.User.Email
                })
                .OrderByDescending(r => r.CreatedOn)
                .ToListAsync();
        }

        public async Task<double> GetAverageRatingAsync(int productId)
        {
            var reviews = await context.DishReviews
                .Where(r => r.ProductId == productId)
                .ToListAsync();

            if (!reviews.Any())
                return 0;

            return reviews.Average(r => r.Rating);
        }

        public async Task<bool> HasUserReviewedAsync(int productId, string userId)
        {
            return await context.DishReviews
                .AnyAsync(r => r.ProductId == productId && r.UserId == userId);
        }

        public async Task AddReviewAsync(DishReviewFormModel model, string userId)
        {
            var review = new DishReview
            {
                ProductId = model.ProductId,
                Rating = model.Rating,
                Comment = model.Comment,
                CreatedOn = DateTime.UtcNow,
                UserId = userId
            };

            await context.DishReviews.AddAsync(review);
            await context.SaveChangesAsync();
        }
        public async Task DeleteReviewAsync(int reviewId)
        {
            var review = await context.DishReviews.FindAsync(reviewId);

            if (review != null)
            {
                context.DishReviews.Remove(review);
                await context.SaveChangesAsync();
            }
        }

    }
}