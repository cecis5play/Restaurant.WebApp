using NUnit.Framework;
using Microsoft.EntityFrameworkCore;
using Restaurant.WebApp.Data;
using Restaurant.WebApp.Data.Entities;
using Restaurant.WebApp.Models;
using Restaurant.WebApp.Services;
using System.Threading.Tasks;
using System.Linq;
using System.Collections.Generic;
using System;
using Microsoft.AspNetCore.Identity;

namespace Restaurant.WebApp.Tests.Services
{
    [TestFixture]
    public class ReviewServiceTests
    {
        private ApplicationDbContext context;
        private ReviewService service;

        [SetUp]
        public void Setup()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: "ReviewServiceTestsDb")
                .Options;

            context = new ApplicationDbContext(options);
            context.Database.EnsureDeleted();
            context.Database.EnsureCreated();

            service = new ReviewService(context);
        }

        [Test]
        public async Task AddReviewAsync_ShouldAddReviewSuccessfully()
        {
            // Arrange
            var model = new DishReviewFormModel
            {
                ProductId = 1,
                Rating = 5,
                Comment = "Perfect dish!"
            };
            string userId = "user-123";

            // Act
            await service.AddReviewAsync(model, userId);

            // Assert
            var review = await context.DishReviews.FirstOrDefaultAsync();
            Assert.IsNotNull(review);
            Assert.AreEqual(model.ProductId, review.ProductId);
            Assert.AreEqual(model.Rating, review.Rating);
            Assert.AreEqual(model.Comment, review.Comment);
            Assert.AreEqual(userId, review.UserId);
        }

        [Test]
        public async Task GetReviewsByProductIdAsync_ShouldReturnCorrectReviews()
        {
            // Arrange
            await context.Users.AddAsync(new IdentityUser { Id = "user-1", Email = "user1@example.com" });
            await context.DishReviews.AddAsync(new DishReview
            {
                ProductId = 10,
                Rating = 4,
                Comment = "Very good!",
                UserId = "user-1",
                CreatedOn = DateTime.UtcNow
            });
            await context.SaveChangesAsync();

            // Act
            var reviews = await service.GetReviewsByProductIdAsync(10);

            // Assert
            Assert.AreEqual(1, reviews.Count());
            Assert.AreEqual("user1@example.com", reviews.First().UserEmail);
        }

        [Test]
        public async Task GetAverageRatingAsync_ShouldReturnCorrectAverage_WhenReviewsExist()
        {
            // Arrange
            await context.DishReviews.AddRangeAsync(new List<DishReview>
            {
                new DishReview { ProductId = 5, Rating = 4 ,UserId = "09091"},
                new DishReview { ProductId = 5, Rating = 5 ,UserId = "09092"}
            });
            await context.SaveChangesAsync();

            // Act
            var average = await service.GetAverageRatingAsync(5);

            // Assert
            Assert.AreEqual(4.5, average);
        }

        [Test]
        public async Task GetAverageRatingAsync_ShouldReturnZero_WhenNoReviews()
        {
            // Act
            var average = await service.GetAverageRatingAsync(99);

            // Assert
            Assert.AreEqual(0, average);
        }

        [Test]
        public async Task HasUserReviewedAsync_ShouldReturnTrue_WhenUserHasReviewed()
        {
            // Arrange
            await context.DishReviews.AddAsync(new DishReview
            {
                ProductId = 2,
                UserId = "user-2",
                Rating = 3
            });
            await context.SaveChangesAsync();

            // Act
            var result = await service.HasUserReviewedAsync(2, "user-2");

            // Assert
            Assert.IsTrue(result);
        }

        [Test]
        public async Task HasUserReviewedAsync_ShouldReturnFalse_WhenUserHasNotReviewed()
        {
            // Act
            var result = await service.HasUserReviewedAsync(3, "user-3");

            // Assert
            Assert.IsFalse(result);
        }

        [Test]
        public async Task DeleteReviewAsync_ShouldDeleteReview_WhenExists()
        {
            // Arrange
            var review = new DishReview
            {
                ProductId = 8,
                Rating = 5,
                Comment = "Awesome!",
                UserId = "user-8"
            };
            await context.DishReviews.AddAsync(review);
            await context.SaveChangesAsync();

            // Act
            await service.DeleteReviewAsync(review.Id);

            // Assert
            var deleted = await context.DishReviews.FindAsync(review.Id);
            Assert.IsNull(deleted);
        }

        [Test]
        public async Task DeleteReviewAsync_ShouldDoNothing_WhenReviewDoesNotExist()
        {
            // Act
            await service.DeleteReviewAsync(9999);

            // Assert
            // Базата трябва да остане празна
            Assert.AreEqual(0, await context.DishReviews.CountAsync());
        }
    }
}