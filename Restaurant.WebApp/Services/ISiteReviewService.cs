using Restaurant.WebApp.Data.Entities;

namespace Restaurant.WebApp.Services
{
    public interface ISiteReviewService
    {
        Task<bool> HasUserReviewedAsync(string userId);
        Task AddReviewAsync(string userId, int rating, string comment);
        Task<List<SiteReview>> GetAllReviewsAsync();
        Task DeleteSiteReviewAsync(int reviewId);

    }
}
