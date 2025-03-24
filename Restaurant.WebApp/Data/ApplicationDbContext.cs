using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Restaurant.WebApp.Data.Entities;

namespace Restaurant.WebApp.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        public DbSet<Reservation> Reservations { get; set; }
        public DbSet<RoomType> RoomTypes { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Product> Products { get; set; }
        public RoomType InsideRoom { get; set; } = null;
        public RoomType OutsideRoom { get; set; } = null;
        public Category Salads { get; set; } = null;
        public Category MainDishes { get; set; } = null;
        public Product Burger { get; set; } = null;
        protected override void OnModelCreating(ModelBuilder builder)
        {
            SeedRoomTypes();
            builder.Entity<RoomType>()
                .HasData(this.InsideRoom,
                        this.OutsideRoom);

            SeedCategories();
            builder.Entity<Category>()
                .HasData(this.Salads,
                        this.MainDishes);
            SeedProducts();
            builder.Entity<Product>()
                .HasData(this.Burger);
            base.OnModelCreating(builder);
        }

        private void SeedRoomTypes()
        {
            this.InsideRoom = new RoomType()
            {
                Id = 1,
                Name = "Вътре в залата",
            };
            this.OutsideRoom = new RoomType()
            {
                Id = 2,
                Name = "Отвън на терасата за пушачи"
            };

        }
        private void SeedCategories()
        {
            this.Salads = new Category()
            {
                Id = 1,
                Name = "Салати",
            };
            this.MainDishes = new Category()
            {
                Id = 2,
                Name = "Основни"
            };

        }

        private void SeedProducts()
        {
            this.Burger = new Product()
            {
                Id = 1,
                Name = "Burger",
                Description = ".....",
                ImageUrl = "https://images.unsplash.com/photo-1568901346375-23c9450c58cd?fm=jpg&q=60&w=3000&ixlib=rb-4.0.3&ixid=M3wxMjA3fDB8MHxzZWFyY2h8Mnx8YnVyZ2VyfGVufDB8fDB8fHww",
                CategoryId = 2,
            };

        }
    }
}
