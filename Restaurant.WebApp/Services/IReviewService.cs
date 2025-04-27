using Restaurant.WebApp.Models;

namespace Restaurant.WebApp.Services
{
    public interface IReviewService
    {
        Task<IEnumerable<DishReviewViewModel>> GetReviewsByProductIdAsync(int productId);
        Task<double> GetAverageRatingAsync(int productId);
        Task<bool> HasUserReviewedAsync(int productId, string userId);
        Task AddReviewAsync(DishReviewFormModel model, string userId);
        Task DeleteReviewAsync(int reviewId);
    }
}
