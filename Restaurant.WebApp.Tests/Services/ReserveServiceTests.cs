using NUnit.Framework;
using Microsoft.EntityFrameworkCore;
using Restaurant.WebApp.Data;
using Restaurant.WebApp.Data.Entities;
using Restaurant.WebApp.Models;
using Restaurant.WebApp.Services;
using System;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;
using Microsoft.AspNetCore.Identity;

namespace Restaurant.WebApp.Tests.Services
{
    [TestFixture]
    public class ReserveServiceTests
    {
        private ApplicationDbContext context;
        private ReserveService service;

        [SetUp]
        public void Setup()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: "ReserveServiceTestsDb")
                .Options;

            context = new ApplicationDbContext(options);
            context.Database.EnsureDeleted();
            context.Database.EnsureCreated();

            service = new ReserveService(context);
        }

        [Test]
        public void Create_ShouldCreateReservation()
        {
            // Arrange
            var model = new ReserveFormViewModel
            {
                ReservationDate = DateTime.UtcNow.AddDays(1),
                PersonNumber = 2,
                ChildrenNumber = 1,
                RoomTypeId = 1
            };
            string userId = "user-1";

            // Act
            int reservationId = service.Create(model, userId);

            // Assert
            var reservation = context.Reservations.FirstOrDefault(r => r.Id == reservationId);
            Assert.IsNotNull(reservation);
            Assert.AreEqual(userId, reservation.UserId);
            Assert.AreEqual(model.PersonNumber, reservation.PersonNumber);
        }


        [Test]
        public void GetUserReservations_ShouldReturnFutureReservationsOnly()
        {
            // Arrange
            var userId = "user-2";
            context.Reservations.AddRange(
                new Reservation { UserId = userId, ReservationDate = DateTime.UtcNow.AddDays(-1), RoomType = new RoomType { Name = "Old Hall" } },
                new Reservation { UserId = userId, ReservationDate = DateTime.UtcNow.AddDays(2), RoomType = new RoomType { Name = "New Hall" } }
            );
            context.SaveChanges();

            // Act
            var reservations = service.GetUserReservations(userId);

            // Assert
            Assert.AreEqual(1, reservations.Count());
            Assert.AreEqual("New Hall", reservations.First().RoomTypeName);
        }

        [Test]
        public void CancelReservation_ShouldRemoveReservation()
        {
            // Arrange
            var userId = "user-3";
            var reservation = new Reservation
            {
                ReservationDate = DateTime.UtcNow.AddDays(1),
                UserId = userId,
                RoomTypeId = 1
            };
            context.Reservations.Add(reservation);
            context.SaveChanges();

            // Act
            service.CancelReservation(reservation.Id, userId);

            // Assert
            var removed = context.Reservations.FirstOrDefault(r => r.Id == reservation.Id);
            Assert.IsNull(removed);
        }

        [Test]
        public void GetAllReservations_ShouldReturnAllReservations()
        {
            // Arrange
            context.Users.Add(new IdentityUser { Id = "user-4", Email = "test@test.com" });
            context.RoomTypes.Add(new RoomType { Id = 7, Name = "Garden" });
            context.SaveChanges();

            context.Reservations.Add(new Reservation
            {
                UserId = "user-4",
                ReservationDate = DateTime.UtcNow.AddDays(1),
                RoomTypeId = 7
            });
            context.SaveChanges();

            // Act
            var reservations = service.GetAllReservations();

            // Assert
            Assert.AreEqual(1, reservations.Count());
            Assert.AreEqual("Garden", reservations.First().RoomTypeName);
        }

        [Test]
        public void GetAllReservations_WithEmailFilter_ShouldReturnCorrectReservations()
        {
            // Arrange
            context.Users.Add(new IdentityUser { Id = "user-5", Email = "filter@test.com" });
            context.RoomTypes.Add(new RoomType { Id = 6, Name = "VIP" });
            context.SaveChanges();

            context.Reservations.Add(new Reservation
            {
                UserId = "user-5",
                ReservationDate = DateTime.UtcNow.AddDays(1),
                RoomTypeId = 6
            });
            context.SaveChanges();

            // Act
            var reservations = service.GetAllReservations("filter");

            // Assert
            Assert.AreEqual(1, reservations.Count());
            Assert.AreEqual("VIP", reservations.First().RoomTypeName);
        }

        [Test]
        public void DeleteReservation_ShouldRemoveReservation()
        {
            // Arrange
            var reservation = new Reservation
            {
                ReservationDate = DateTime.UtcNow.AddDays(1),
                RoomTypeId = 1,
                UserId = "user-6"
            };
            context.Reservations.Add(reservation);
            context.SaveChanges();

            // Act
            service.DeleteReservation(reservation.Id);

            // Assert
            var deleted = context.Reservations.FirstOrDefault(r => r.Id == reservation.Id);
            Assert.IsNull(deleted);
        }
    }
}