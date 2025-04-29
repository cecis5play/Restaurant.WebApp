using NUnit.Framework;
using Microsoft.EntityFrameworkCore;
using Restaurant.WebApp.Data;
using Restaurant.WebApp.Data.Entities;
using Restaurant.WebApp.Services;
using System.Threading.Tasks;
using System.Linq;
using System.Collections.Generic;

namespace Restaurant.WebApp.Tests.Services
{
    [TestFixture]
    public class SiteReviewServiceTests
    {
        private ApplicationDbContext context;
        private SiteReviewService service;

        [SetUp]
        public void Setup()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: "SiteReviewServiceTestsDb")
                .Options;

            context = new ApplicationDbContext(options);
            context.Database.EnsureDeleted();
            context.Database.EnsureCreated();

            service = new SiteReviewService(context);
        }

        [Test]
        public async Task AddReviewAsync_ShouldAddReviewSuccessfully()
        {
            // Arrange
            string userId = "test-user";
            int rating = 5;
            string comment = "Great site!";

            // Act
            await service.AddReviewAsync(userId, rating, comment);

            // Assert
            var review = await context.SiteReviews.FirstOrDefaultAsync();
            Assert.IsNotNull(review);
            Assert.AreEqual(userId, review.UserId);
            Assert.AreEqual(rating, review.Rating);
            Assert.AreEqual(comment, review.Comment);
        }

        [Test]
        public async Task HasUserReviewedAsync_ShouldReturnTrue_IfUserReviewed()
        {
            // Arrange
            string userId = "reviewed-user";
            await service.AddReviewAsync(userId, 4, "Good!");

            // Act
            var hasReviewed = await service.HasUserReviewedAsync(userId);

            // Assert
            Assert.IsTrue(hasReviewed);
        }

        [Test]
        public async Task HasUserReviewedAsync_ShouldReturnFalse_IfUserHasNotReviewed()
        {
            // Act
            var hasReviewed = await service.HasUserReviewedAsync("nonexistent-user");

            // Assert
            Assert.IsFalse(hasReviewed);
        }

        [Test]
        public async Task GetAllReviewsAsync_ShouldReturnReviewsOrderedByDateDescending()
        {
            // Arrange
            await service.AddReviewAsync("user1", 3, "Nice");
            await Task.Delay(10); // Малко забавяне за да имат различни дати
            await service.AddReviewAsync("user2", 5, "Excellent");

            // Act
            var reviews = await service.GetAllReviewsAsync();

            // Assert
            Assert.AreEqual(2, reviews.Count);
            Assert.AreEqual("user2", reviews.First().UserId); // Последният добавен трябва да е пръв
        }

        [Test]
        public async Task DeleteSiteReviewAsync_ShouldDeleteReview_WhenReviewExists()
        {
            // Arrange
            await service.AddReviewAsync("user-to-delete", 2, "Bad review");
            var review = await context.SiteReviews.FirstOrDefaultAsync();

            // Act
            await service.DeleteSiteReviewAsync(review.Id);

            // Assert
            var deleted = await context.SiteReviews.FindAsync(review.Id);
            Assert.IsNull(deleted);
        }

        [Test]
        public async Task DeleteSiteReviewAsync_ShouldDoNothing_WhenReviewDoesNotExist()
        {
            // Arrange
            int fakeReviewId = 999;

            // Act
            await service.DeleteSiteReviewAsync(fakeReviewId);

            // Assert
            Assert.AreEqual(0, await context.SiteReviews.CountAsync());
        }
    }
}