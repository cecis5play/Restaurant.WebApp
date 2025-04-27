using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Restaurant.WebApp.Data;
using Restaurant.WebApp.Data.Entities;
using Restaurant.WebApp.Services;

namespace Restaurant.WebApp
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            var connectionString = builder.Configuration.GetConnectionString("DefaultConnection") ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");
            builder.Services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(connectionString));
            builder.Services.AddDatabaseDeveloperPageExceptionFilter();

            builder.Services.AddDefaultIdentity<IdentityUser>(options => {
                options.SignIn.RequireConfirmedAccount = false;
                options.Password.RequireDigit = false;
                options.Password.RequireUppercase = false;
                options.Password.RequireLowercase = false;
                options.Password.RequireNonAlphanumeric = false; })
                .AddRoles<IdentityRole>()
                .AddEntityFrameworkStores<ApplicationDbContext>();
            builder.Services.AddControllersWithViews();
            builder.Services.AddTransient<IMenuService, MenuService>();
            builder.Services.AddTransient<IReserveService, ReserveService>();
            builder.Services.AddTransient<IOrderService, OrderService>();
            builder.Services.AddTransient<IReviewService, ReviewService>();
            builder.Services.AddTransient<ISiteReviewService, SiteReviewService>();
            builder.Services.AddTransient<ApplicationDbContext>();
            builder.Services.AddControllersWithViews();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseMigrationsEndPoint();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");
            app.MapRazorPages();

            using (var scope = app.Services.CreateScope())
            {
                var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();
                var roles = new[] { "Admin", "Manager" };


                foreach (var role in roles)
                {
                    if (!await roleManager.RoleExistsAsync(role))
                    {
                        await roleManager.CreateAsync(new IdentityRole(role));
                    }
                }
            }

            using (var scope = app.Services.CreateScope())
            {

                var userManager = scope.ServiceProvider.GetRequiredService<UserManager<IdentityUser>>();
                string email = "admin@admin.com";
                string password = "Admin123";

                if (await userManager.FindByEmailAsync(email) == null)
                {
                    var user = new IdentityUser();
                    user.UserName = email;
                    user.Email = email;

                    await userManager.CreateAsync(user, password);
                    await userManager.AddToRoleAsync(user, "Admin");
                }

            }

            using (var scope = app.Services.CreateScope())
            {

                var userManager = scope.ServiceProvider.GetRequiredService<UserManager<IdentityUser>>();
                string email = "manager@manager.com";
                string password = "Manager123";

                if (await userManager.FindByEmailAsync(email) == null)
                {
                    var user = new IdentityUser();
                    user.UserName = email;
                    user.Email = email;

                    await userManager.CreateAsync(user, password);
                    await userManager.AddToRoleAsync(user, "Manager");
                }

            }

            app.Run();
        }
    }
}
