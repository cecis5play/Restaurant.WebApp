using Restaurant.WebApp.Data.Entities;
using Restaurant.WebApp.Models;

namespace Restaurant.WebApp.Services
{
    public interface IOrderService
    {
        public int GetCartItemCount(string userId);
        void AddToCart(int productId, string userId);
        void PlaceOrder(string userId);
        IEnumerable<CartItem> GetCartItems(string userId);
        Order GetLastOrder(string userId);
        public void IncreaseQuantity(int cartItemId);
        public void DecreaseQuantity(int cartItemId);
        public void RemoveFromCart(int cartItemId);
        IEnumerable<Order> GetOrders(string userId);
        void DeleteOrder(int orderId);
        public IEnumerable<Order> GetAllOrders();
    }
}
