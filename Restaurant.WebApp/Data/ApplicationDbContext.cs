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
        public Category Meat { get; set; } = null;
        public Category Fish { get; set; } = null;
        public Category Pasta { get; set; } = null;
        public Category Pizza { get; set; } = null;
        public Category Sushi { get; set; } = null;
        public Category SideDishes { get; set; } = null;
        public Category Deserts { get; set; } = null;
        public Category VegeterianDishes { get; set; } = null;
        public Product AvocadoSalad { get; set; } = null;
        public Product CesarSalad { get; set; } = null;
        public Product TunaSalad { get; set; } = null;
        public Product GreekSalad { get; set; } = null;
        public Product Pork { get; set; } = null;
        public Product Beef { get; set; } = null;
        public Product Meetballs { get; set; } = null;
        public Product ChikenFilet { get; set; } = null;
        public Product Carbonara { get; set; } = null;
        public Product Bolognese { get; set; } = null;
        public Product SeafoodPasta { get; set; } = null;
        public Product Lasagna { get; set; } = null;
        public Product Mozzarella { get; set; } = null;
        public Product MeatballPizza { get; set; } = null;
        public Product Fourcheese { get; set; } = null;
        public Product Salami { get; set; } = null;
        public Product Salmon { get; set; } = null;
        public Product GrilledFish { get; set; } = null;
        public Product Shrimp { get; set; } = null;
        public Product FriedFish { get; set; } = null;
        public Product FutomakiCrab { get; set; } = null;
        public Product NigiriSalmon { get; set; } = null;
        public Product Hosomaki { get; set; } = null;
        public Product FriedRolls { get; set; } = null;
        public Product FrenchFries { get; set; } = null;
        public Product GreenBeans { get; set; } = null;
        public Product RoastPotatos { get; set; } = null;
        public Product BakedVeggies { get; set; } = null;
        public Product Cheesecake { get; set; } = null;
        public Product Tiramisu { get; set; } = null;
        public Product Pancake { get; set; } = null;
        public Product Waffles { get; set; } = null;
        public Product Tofu { get; set; } = null;
        public Product Dumplings { get; set; } = null;
        public Product EggToast { get; set; } = null;
        public Product SpringRolls { get; set; } = null;
        protected override void OnModelCreating(ModelBuilder builder)
        {
            SeedRoomTypes();
            builder.Entity<RoomType>()
                .HasData(this.InsideRoom,
                        this.OutsideRoom);

            SeedCategories();
            builder.Entity<Category>()
                .HasData(this.Salads,
                        this.Meat,
                        this.Fish,
                        this.Pasta,
                        this.Pizza,
                        this.Sushi,
                        this.SideDishes,
                        this.Deserts,
                        this.VegeterianDishes);
            SeedProducts();
            builder.Entity<Product>()
                .HasData(
                this.AvocadoSalad,
                this.CesarSalad,
                this.TunaSalad,
                this.GreekSalad,
                this.Pork,
                this.Beef,
                this.Meetballs,
                this.ChikenFilet,
                this.Carbonara,
                this.Bolognese,
                this.SeafoodPasta,
                this.Lasagna,
                this.Mozzarella,
                this.MeatballPizza,
                this.Fourcheese,
                this.Salami,
                this.Salmon,
                this.GrilledFish,
                this.Shrimp,
                this.FriedFish,
                this.FutomakiCrab,
                this.NigiriSalmon,
                this.Hosomaki,
                this.FriedRolls,
                this.FrenchFries,
                this.GreenBeans,
                this.RoastPotatos,
                this.BakedVeggies,
                this.Cheesecake,
                this.Tiramisu,
                this.Pancake,
                this.Waffles,
                this.Tofu,
                this.Dumplings,
                this.EggToast,
                this.SpringRolls);
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
            this.Meat = new Category()
            {
                Id = 2,
                Name = "Месо"
            };
            this.Fish = new Category()
            {
                Id = 3,
                Name = "Риба"
            };
            this.Pasta = new Category()
            {
                Id = 4,
                Name = "Паста"
            };
            this.Pizza = new Category()
            {
                Id = 5,
                Name = "Пица"
            };
            this.Sushi = new Category()
            {
                Id = 6,
                Name = "Суши"
            };
            this.SideDishes = new Category()
            {
                Id = 7,
                Name = "Странични ястия"
            };
            this.Deserts = new Category()
            {
                Id = 8,
                Name = "Десерти"
            };
            this.VegeterianDishes = new Category()
            {
                Id = 9,
                Name = "Веган"
            };

        }

        private void SeedProducts()
        {
            this.AvocadoSalad = new Product()
            {
                Id = 1,
                Name = "Салата с авокадо",
                Description = "салата, авокадо",
                ImageUrl = "https://i.imgur.com/ytAl6b7.jpg",
                CategoryId = 1,
            };
            this.CesarSalad = new Product()
            {
                Id = 2,
                Name = "Салата цезър",
                Description = "салата, крутони, пиле",
                ImageUrl = "https://i.imgur.com/46hWRKm.jpg",
                CategoryId = 1,
            };
            this.TunaSalad = new Product()
            {
                Id = 3,
                Name = "Салата с риба тон",
                Description = "салата, риба тон",
                ImageUrl = "https://i.imgur.com/nRqrS2Z.jpg",
                CategoryId = 1,
            };
            this.GreekSalad = new Product()
            {
                Id = 4,
                Name = "Гръцка салата",
                Description = "салата, лук, домати, краставица",
                ImageUrl = "https://i.imgur.com/tGkD6Mz.jpg",
                CategoryId = 1,
            };
            this.Pork = new Product()
            {
                Id = 5,
                Name = "Свинско",
                Description = "свинско месо с зеленчуци",
                ImageUrl = "https://i.imgur.com/Mud7P5Z.jpg",
                CategoryId = 2,
            };
            this.Meetballs = new Product()
            {
                Id = 6,
                Name = "Кюфтета",
                Description = "кюфтета с картофи и зеленчуци",
                ImageUrl = "https://i.imgur.com/eZEPgd8.jpg",
                CategoryId = 2,
            };
            this.ChikenFilet = new Product()
            {
                Id = 7,
                Name = "Пилешко месо",
                Description = "пилешко месо, зеленчуци",
                ImageUrl = "https://i.imgur.com/4HNR1ZY.jpg",
                CategoryId = 2,
            };
            this.Beef = new Product()
            {
                Id = 8,
                Name = "Телешко",
                Description = "Телешко месо с картофи",
                ImageUrl = "https://i.imgur.com/HeE8bQ5.jpg",
                CategoryId = 2,
            };
            this.Carbonara = new Product()
            {
                Id = 9,
                Name = "Паста Карбонара",
                Description = "Карбонара",
                ImageUrl = "https://i.imgur.com/n4TbgT7.jpg",
                CategoryId = 4,
            };
            this.Bolognese = new Product()
            {
                Id = 10,
                Name = "Паста Болонезе",
                Description = "Болонезе",
                ImageUrl = "https://i.imgur.com/ZG42J7n.jpg",
                CategoryId = 4,
            };
            this.SeafoodPasta = new Product()
            {
                Id = 11,
                Name = "Паста с морски дарове",
                Description = "морски дарове с паста",
                ImageUrl = "https://i.imgur.com/ea3XILH.jpg",
                CategoryId = 4,
            };
            this.Lasagna = new Product()
            {
                Id = 12,
                Name = "Лазаня",
                Description = "кайма, кашкавал, босилек",
                ImageUrl = "https://i.imgur.com/tWo1D41.jpg",
                CategoryId = 4,
            };
            this.Mozzarella = new Product()
            {
                Id = 13,
                Name = "Пица с моцарела",
                Description = "моцарела, доматен сос",
                ImageUrl = "https://i.imgur.com/mabeII0.jpg",
                CategoryId = 5,
            };
            this.MeatballPizza = new Product()
            {
                Id = 14,
                Name = "Пица с кюфтета",
                Description = "кюфтета, доматен сос, кашкавал",
                ImageUrl = "https://i.imgur.com/mZItQiX.jpg",
                CategoryId = 5,
            };
            this.Fourcheese = new Product()
            {
                Id = 15,
                Name = "Пица четири сирена",
                Description = "сирена от различен вид",
                ImageUrl = "https://i.imgur.com/91ey2lO.jpg",
                CategoryId = 5,
            };
            this.Salami = new Product()
            {
                Id = 16,
                Name = "Пица салами",
                Description = "салам, чери домати, босиляк",
                ImageUrl = "https://i.imgur.com/mQ8m6YF.jpg",
                CategoryId = 5,
            };
            this.Salmon = new Product()
            {
                Id = 17,
                Name = "Сьомга",
                Description = "сьомга на грил",
                ImageUrl = "https://i.imgur.com/Iw86J6D.jpg",
                CategoryId = 3,
            };
            this.GrilledFish = new Product()
            {
                Id = 18,
                Name = "Риба на скара",
                Description = "с резънчета лимон и салата",
                ImageUrl = "https://i.imgur.com/uWiso3I.jpg",
                CategoryId = 3,
            };
            this.Shrimp = new Product()
            {
                Id = 19,
                Name = "Скариди",
                Description = "порция изчистени скариди",
                ImageUrl = "https://i.imgur.com/maOOr2c.jpg",
                CategoryId = 3,
            };
            this.FriedFish = new Product()
            {
                Id = 20,
                Name = "Пържена риба",
                Description = "пържена риба с картофено пюре",
                ImageUrl = "https://i.imgur.com/H35uMie.jpg",
                CategoryId = 3,
            };
            this.FutomakiCrab = new Product()
            {
                Id = 21,
                Name = "Футомаки Рак",
                Description = "Суши стил футомаки с рак",
                ImageUrl = "https://i.imgur.com/I3HdRmg.png",
                CategoryId = 6,
            };
            this.NigiriSalmon = new Product()
            {
                Id = 22,
                Name = "Нигири сьомга",
                Description = "Суши стил нигири със сьомга",
                ImageUrl = "https://i.imgur.com/9PELxEu.png",
                CategoryId = 6,
            };
            this.Hosomaki = new Product()
            {
                Id = 23,
                Name = "Хосумаки сет",
                Description = "Суши сет стил хосумаки",
                ImageUrl = "https://i.imgur.com/BqDTtuo.png",
                CategoryId = 6,
            };
            this.FriedRolls = new Product()
            {
                Id = 24,
                Name = "Пържен сет",
                Description = "Суши пържен сет",
                ImageUrl = "https://i.imgur.com/kGfrZXO.png",
                CategoryId = 6,
            };
            this.FrenchFries = new Product()
            {
                Id = 25,
                Name = "Пържени картофи",
                Description = "Прясно изпържени картофи с кашкавалена покривка",
                ImageUrl = "https://i.imgur.com/LEzjncV.png",
                CategoryId = 7,
            };
            this.GreenBeans = new Product()
            {
                Id = 26,
                Name = "Зелен боб",
                Description = "боб зелен соров",
                ImageUrl = "https://i.imgur.com/35Rfcar.jpg",
                CategoryId = 7,
            };
            this.RoastPotatos = new Product()
            {
                Id = 27,
                Name = "Печени картофи",
                Description = "изпечени прясни картофи",
                ImageUrl = "https://i.imgur.com/afBYXEq.jpg",
                CategoryId = 7,
            };
            this.BakedVeggies = new Product()
            {
                Id = 28,
                Name = "Печени зеленчуци",
                Description = "Прясно изпечени зеленчуци",
                ImageUrl = "https://i.imgur.com/wrkStpo.jpg",
                CategoryId = 7,
            };
            this.Cheesecake = new Product()
            {
                Id = 29,
                Name = "Чийскейк",
                Description = "пърче чийскейк",
                ImageUrl = "https://i.imgur.com/b98zxYM.jpg",
                CategoryId = 8,
            };
            this.Tiramisu = new Product()
            {
                Id = 30,
                Name = "Тирамису",
                Description = "Домашно приготвено тирамису",
                ImageUrl = "https://i.imgur.com/ucIJMQO.jpg",
                CategoryId = 8,
            };
            this.Pancake = new Product()
            {
                Id = 31,
                Name = "Палачинки",
                Description = "Палачинки с шоколад",
                ImageUrl = "https://i.imgur.com/16gDj6l.jpg",
                CategoryId = 8,
            };
            this.Waffles = new Product()
            {
                Id = 32,
                Name = "Гофрети",
                Description = "Гофрети с кленов сироп",
                ImageUrl = "https://i.imgur.com/nY9Cgqf.jpg",
                CategoryId = 8,
            };
            this.Tofu = new Product()
            {
                Id = 33,
                Name = "Тофу",
                Description = "Тофу салата",
                ImageUrl = "https://i.imgur.com/D2nrUPV.jpg",
                CategoryId = 9,
            };
            this.Dumplings = new Product()
            {
                Id = 34,
                Name = "Дъмплинги",
                Description = "дъмплинги по китайски",
                ImageUrl = "https://i.imgur.com/mqmYr9T.jpg",
                CategoryId = 9,
            };
            this.EggToast = new Product()
            {
                Id = 35,
                Name = "Бъркани яйца с тостове",
                Description = "2 яца по стил бъркани",
                ImageUrl = "https://i.imgur.com/co7Mn3A.jpg",
                CategoryId = 9,
            };
            this.SpringRolls = new Product()
            {
                Id = 36,
                Name = "Запържени ролца",
                Description = "със сос сладко кисел и вътрешност",
                ImageUrl = "https://i.imgur.com/CbdHjAT.jpg",
                CategoryId = 9,
            };

        }
    }
}
