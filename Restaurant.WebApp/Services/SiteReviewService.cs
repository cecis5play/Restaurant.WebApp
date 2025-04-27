using Restaurant.WebApp.Data;
using Restaurant.WebApp.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Restaurant.WebApp.Services;

public class SiteReviewService : ISiteReviewService
{
    private readonly ApplicationDbContext context;

    public SiteReviewService(ApplicationDbContext context)
    {
        this.context = context;
    }

    public async Task<bool> HasUserReviewedAsync(string userId)
    {
        return await context.SiteReviews.AnyAsync(x => x.UserId == userId);
    }

    public async Task AddReviewAsync(string userId, int rating, string comment)
    {
        var review = new SiteReview
        {
            UserId = userId,
            Rating = rating,
            Comment = comment
        };

        await context.SiteReviews.AddAsync(review);
        await context.SaveChangesAsync();
    }

    public async Task<List<SiteReview>> GetAllReviewsAsync()
    {
        return await context.SiteReviews
            .OrderByDescending(r => r.CreatedOn)
            .ToListAsync();
    }
    public async Task DeleteSiteReviewAsync(int reviewId)
    {
        var review = await context.SiteReviews.FindAsync(reviewId);

        if (review != null)
        {
            context.SiteReviews.Remove(review);
            await context.SaveChangesAsync();
        }
    }
}