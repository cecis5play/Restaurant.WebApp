using NUnit.Framework;
using Microsoft.EntityFrameworkCore;
using Restaurant.WebApp.Data;
using Restaurant.WebApp.Data.Entities;
using Restaurant.WebApp.Services;
using System;
using System.Linq;
using System.Collections.Generic;

namespace Restaurant.WebApp.Tests.Services
{
    [TestFixture]
    public class OrderServiceTests
    {
        private ApplicationDbContext context;
        private OrderService service;

        [SetUp]
        public void Setup()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: "OrderServiceTestsDb")
                .Options;

            context = new ApplicationDbContext(options);
            context.Database.EnsureDeleted();
            context.Database.EnsureCreated();

            service = new OrderService(context);
        }

        [Test]
        public void AddToCart_ShouldAddNewCartItem()
        {
            var userId = "user1";
            var product = new Product { Id = 1000, Name = "Pizza", Description = "Description", ImageUrl = "ImageUrl" };
            context.Products.Add(product);
            context.SaveChanges();

            service.AddToCart(1, userId);

            var cartItem = context.CartItems.FirstOrDefault();
            Assert.IsNotNull(cartItem);
            Assert.AreEqual(1, cartItem.Quantity);
        }

        [Test]
        public void AddToCart_ShouldIncreaseQuantityIfExists()
        {
            var userId = "user2";
            var product = new Product { Id = 1001, Name = "Pasta", Description = "Description", ImageUrl = "ImageUrl" };
            context.Products.Add(product);
            context.CartItems.Add(new CartItem { ProductId = 1001, UserId = userId, Quantity = 1 });
            context.SaveChanges();

            service.AddToCart(1001, userId);

            var cartItem = context.CartItems.First();
            Assert.AreEqual(2, cartItem.Quantity);
        }

        [Test]
        public void PlaceOrder_ShouldCreateOrderAndClearCart()
        {
            var userId = "user3";
            var product = new Product { Id = 1003, Name = "Burger", Description = "Description", ImageUrl = "ImageUrl" };
            context.Products.Add(product);
            context.CartItems.Add(new CartItem { ProductId = 1003, UserId = userId, Quantity = 2 });
            context.SaveChanges();

            service.PlaceOrder(userId);

            var order = context.Orders.Include(o => o.OrderItems).FirstOrDefault();
            Assert.IsNotNull(order);
            Assert.AreEqual(1, order.OrderItems.Count);
            Assert.AreEqual(0, context.CartItems.Count());
        }

        [Test]
        public void GetCartItems_ShouldReturnCartItems()
        {
            var userId = "user4";
            context.Products.Add(new Product { Id = 1002, Name = "Sushi", Description = "Description", ImageUrl = "ImageUrl" });
            context.CartItems.Add(new CartItem { ProductId = 1002, UserId = userId, Quantity = 1 });
            context.SaveChanges();

            var items = service.GetCartItems(userId);

            Assert.AreEqual(1, items.Count());
        }

        [Test]
        public void GetLastOrder_ShouldReturnMostRecentOrder()
        {
            var userId = "user5";
            context.Orders.Add(new Order { UserId = userId, OrderDate = DateTime.UtcNow.AddMinutes(-10) });
            context.Orders.Add(new Order { UserId = userId, OrderDate = DateTime.UtcNow });
            context.SaveChanges();

            var lastOrder = service.GetLastOrder(userId);

            Assert.IsNotNull(lastOrder);
            Assert.AreEqual(userId, lastOrder.UserId);
        }

        [Test]
        public void IncreaseQuantity_ShouldIncrementQuantity()
        {
            var cartItem = new CartItem { ProductId = 5, UserId = "user6", Quantity = 1 };
            context.CartItems.Add(cartItem);
            context.SaveChanges();

            service.IncreaseQuantity(cartItem.Id);

            var updatedItem = context.CartItems.First();
            Assert.AreEqual(2, updatedItem.Quantity);
        }

        [Test]
        public void DecreaseQuantity_ShouldDecrementQuantity()
        {
            var cartItem = new CartItem { ProductId = 6, UserId = "user7", Quantity = 2 };
            context.CartItems.Add(cartItem);
            context.SaveChanges();

            service.DecreaseQuantity(cartItem.Id);

            var updatedItem = context.CartItems.First();
            Assert.AreEqual(1, updatedItem.Quantity);
        }

        [Test]
        public void DecreaseQuantity_ShouldRemoveItemIfQuantityOne()
        {
            var cartItem = new CartItem { ProductId = 7, UserId = "user8", Quantity = 1 };
            context.CartItems.Add(cartItem);
            context.SaveChanges();

            service.DecreaseQuantity(cartItem.Id);

            var exists = context.CartItems.Any();
            Assert.IsFalse(exists);
        }

        [Test]
        public void RemoveFromCart_ShouldDeleteCartItem()
        {
            var cartItem = new CartItem { ProductId = 8, UserId = "user9", Quantity = 1 };
            context.CartItems.Add(cartItem);
            context.SaveChanges();

            service.RemoveFromCart(cartItem.Id);

            Assert.AreEqual(0, context.CartItems.Count());
        }

        [Test]
        public void GetOrders_ShouldReturnUserOrdersAndUpdateStatus()
        {
            var userId = "user10";
            var order = new Order
            {
                UserId = userId,
                OrderDate = DateTime.UtcNow.AddMinutes(-50),
                Status = "Доставя се",
                OrderItems = new List<OrderItem>()
            };
            context.Orders.Add(order);
            context.SaveChanges();

            var orders = service.GetOrders(userId);

            Assert.AreEqual(1, orders.Count());
            Assert.AreEqual("Доставена", orders.First().Status);
        }

        [Test]
        public void DeleteOrder_ShouldRemoveOrder()
        {
            var order = new Order { UserId = "user11", OrderDate = DateTime.UtcNow };
            context.Orders.Add(order);
            context.SaveChanges();

            service.DeleteOrder(order.Id);

            var exists = context.Orders.Any();
            Assert.IsFalse(exists);
        }

        [Test]
        public void GetAllOrders_ShouldReturnAllOrders()
        {
            context.Orders.Add(new Order { UserId = "user12", OrderDate = DateTime.UtcNow });
            context.SaveChanges();

            var allOrders = service.GetAllOrders();

            Assert.AreEqual(1, allOrders.Count());
        }

        [Test]
        public void GetCartItemCount_ShouldReturnTotalItems()
        {
            var userId = "user13";
            context.CartItems.AddRange(
                new CartItem { ProductId = 9, UserId = userId, Quantity = 2 },
                new CartItem { ProductId = 10, UserId = userId, Quantity = 3 }
            );
            context.SaveChanges();

            var count = service.GetCartItemCount(userId);

            Assert.AreEqual(5, count);
        }
    }
}