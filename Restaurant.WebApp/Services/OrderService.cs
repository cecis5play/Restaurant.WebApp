using Restaurant.WebApp.Data.Entities;
using Restaurant.WebApp.Data;
using Microsoft.EntityFrameworkCore;
using Restaurant.WebApp.Models;

namespace Restaurant.WebApp.Services
{
    public class OrderService : IOrderService
    {
        private readonly ApplicationDbContext context;

        public OrderService(ApplicationDbContext context)
        {
            this.context = context;
        }

        public void AddToCart(int productId, string userId)
        {
            var cartItem = context.CartItems.FirstOrDefault(ci => ci.ProductId == productId && ci.UserId == userId);
            if (cartItem != null)
            {
                cartItem.Quantity++;
            }
            else
            {
                context.CartItems.Add(new CartItem { ProductId = productId, Quantity = 1, UserId = userId });
            }
            context.SaveChanges();
        }

        public void PlaceOrder(string userId)
        {
            var cartItems = context.CartItems.Where(ci => ci.UserId == userId).ToList();
            var order = new Order { UserId = userId, OrderDate = DateTime.Now, OrderItems = new List<OrderItem>() };

            foreach (var cartItem in cartItems)
            {
                order.OrderItems.Add(new OrderItem { ProductId = cartItem.ProductId, Quantity = cartItem.Quantity });
                context.CartItems.Remove(cartItem);
            }

            context.Orders.Add(order);
            context.SaveChanges();
        }

        public IEnumerable<CartItem> GetCartItems(string userId)
        {
            return context.CartItems.Include(ci => ci.Product).Where(ci => ci.UserId == userId).ToList();
        }
        public Order GetLastOrder(string userId)
        {
            return context.Orders
                           .Include(o => o.OrderItems)
                           .ThenInclude(oi => oi.Product)
                           .Where(o => o.UserId == userId)
                           .OrderByDescending(o => o.OrderDate)
                           .FirstOrDefault();
        }
        public void IncreaseQuantity(int cartItemId)
        {
            var cartItem = context.CartItems.Find(cartItemId);
            if (cartItem != null)
            {
                cartItem.Quantity++;
                context.SaveChanges();
            }
        }

        public void DecreaseQuantity(int cartItemId)
        {
            var cartItem = context.CartItems.Find(cartItemId);
            if (cartItem != null)
            {
                if (cartItem.Quantity > 1)
                {
                    cartItem.Quantity--;
                }
                else
                {
                    context.CartItems.Remove(cartItem); 
                }
                context.SaveChanges();
            }
        }

        public void RemoveFromCart(int cartItemId)
        {
            var cartItem = context.CartItems.Find(cartItemId);
            if (cartItem != null)
            {
                context.CartItems.Remove(cartItem);
                context.SaveChanges();
            }
        }
    }
}
