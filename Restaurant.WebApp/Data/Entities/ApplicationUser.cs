using Microsoft.AspNetCore.Identity;

namespace Restaurant.WebApp.Data.Entities
{
    public class ApplicationUser : IdentityUser
    {

        public ICollection<Product> Order { get; set; }
        public ICollection<Reservation> Reservations { get; set; }
        public ICollection<Cart> Carts { get; set; }
    }
}
