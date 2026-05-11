using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Cheezza.Models; // Pizza modelini görebilmesi için

namespace Cheezza.Data
{
    // IdentityDbContext yanına <ApplicationUser> eklemek, 
    // Identity tablolarının senin özel kullanıcı sınıfınla çalışmasını sağlar.
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        // 🍕 Pizza tablonu veritabanına ekleyen satır
        public DbSet<Pizza> Pizzas { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            // Admin panelini test etmek için başlangıçta birkaç pizza ekleyelim (Opsiyonel)
            builder.Entity<Pizza>().HasData(
                new Pizza { Id = 1, Name = "Margarita", Price = 250, Description = "Mozzarella ve fesleğen" },
                new Pizza { Id = 2, Name = "Pepperoni", Price = 300, Description = "Sucuklu ve baharatlı" }
            );
        }
    }
}