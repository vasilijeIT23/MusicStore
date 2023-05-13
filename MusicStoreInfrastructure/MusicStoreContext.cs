using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using MusicStoreCore.Entities;
using System.Reflection;

namespace MusicStoreInfrastructure
{
    public class MusicStoreContext : DbContext
    {
        public MusicStoreContext(DbContextOptions options, IConfiguration configuration) : base(options)
        {
        }

        DbSet<Customer> Customers { get; set; }
        DbSet<Product> Products { get; set; }
        DbSet<ProductType> ProductTypes { get; set; }
        DbSet<Warehouse> Warehouses { get; set; }
        DbSet<Stock> Stocks { get; set; }
        DbSet<Cart> Carts { get; set; }
        DbSet<CartItem> CartItems { get; set; }
        DbSet<Order> Orders { get; set; }
        DbSet<OrderItem> OrderItems { get; set; }
        DbSet<Review> Reviews { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }
    }
}
