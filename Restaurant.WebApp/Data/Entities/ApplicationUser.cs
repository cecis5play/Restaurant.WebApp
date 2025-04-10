using Microsoft.AspNetCore.Identity;

namespace Restaurant.WebApp.Data.Entities
{
    public class ApplicationUser : IdentityUser
    {

        public ICollection<Product> Products { get; set; }
        public ICollection<Reservation> Reservations { get; set; }
    }
}
