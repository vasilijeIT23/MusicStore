using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using MusicStoreCore.Entities;
using MusicStoreCore.Enums;
using System.Diagnostics;
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

            //customers
            modelBuilder.Entity<Customer>().HasData(new
            {
                Id = Guid.NewGuid(),
                FirstName = "Marko",
                LastName = "Markovic",
                Email = "marko@ds.com",
                Status = Status.Regular,
                Role = Role.Regular,
                StatusExpirationDate = DateTime.Now.AddDays(-100),
                MoneySpent = 0.0d,
                Orders = new List<Order>(),
                Reviews = new List<Review>()
            });

            modelBuilder.Entity<Customer>().HasData(new
            {
                Id = Guid.NewGuid(),
                FirstName = "Vasilije",
                LastName = "Mucibabic",
                Email = "vasilije.mucibabic@gmail.com",
                Status = Status.Regular,
                Role = Role.Admin,
                StatusExpirationDate = DateTime.Now.AddDays(-100),
                MoneySpent = 0.0d,
                Orders = new List<Order>(),
                Reviews = new List<Review>()
            });

            //warehouse
            modelBuilder.Entity<Warehouse>().HasData(new
            {
                Id = Guid.NewGuid(),
                Name = "Warehouse_1",
                Capacity = 10000
            });
            
            //product types
            modelBuilder.Entity<ProductType>().HasData(new
            {
                Id = Guid.NewGuid(),
                Category = Category.Instrument,
                Name = "Gitara"
            }) ;

            modelBuilder.Entity<ProductType>().HasData(new
            {
                Id = Guid.NewGuid(),
                Category = Category.Instrument,
                Name = "Harmonika"
            });

            modelBuilder.Entity<ProductType>().HasData(new
            {
                Id = Guid.NewGuid(),
                Category = Category.Instrument,
                Name = "Flauta"
            });

            modelBuilder.Entity<ProductType>().HasData(new
            {
                Id = Guid.NewGuid(),
                Category = Category.Instrument,
                Name = "Zvucnik"
            });

            modelBuilder.Entity<ProductType>().HasData(new
            {
                Id = Guid.NewGuid(),
                Category = Category.Instrument,
                Name = "Pojacalo"
            });
            
            /*
            //products
            modelBuilder.Entity<Product>().HasData(new
            {
                Id = Guid.NewGuid(),
                Name = "Fender Stratocaster",
                InStock = true,
                Price = 500.0d,
                ProductType = new 
                {
                    Id = Guid.NewGuid(), 
                    Category = Category.Instrument, 
                    Name = "Gitara"
                },
                Reviews = new List<Review>(),
            });

            modelBuilder.Entity<Product>().HasData(new
            {
                Id = Guid.NewGuid(),
                Name = "Pigini 5874",
                InStock = true,
                Price = 10000.0d,
                ProductType = new
                {
                    Id = Guid.NewGuid(),
                    Category = Category.Instrument,
                    Name = "Harmonika"
                },
                Reviews = new List<Review>(),
            });

            modelBuilder.Entity<Product>().HasData(new
            {
                Id = Guid.NewGuid(),
                Name = "Yamaha 1000",
                InStock = true,
                Price = 1000.0d,
                ProductType = new
                {
                    Id = Guid.NewGuid(),
                    Category = Category.Instrument,
                    Name = "Flauta"
                },
                Reviews = new List<Review>(),
            });

            modelBuilder.Entity<Product>().HasData(new
            {
                Id = Guid.NewGuid(),
                Name = "JBL 450",
                InStock = true,
                Price = 100.0d,
                ProductType = new
                {
                    Id = Guid.NewGuid(),
                    Category = Category.Instrument,
                    Name = "Zvucnik"
                },
                Reviews = new List<Review>(),
            });

            modelBuilder.Entity<Product>().HasData(new
            {
                Id = Guid.NewGuid(),
                Name = "Fender enhancer",
                InStock = true,
                Price = 250.0d,
                ProductType = new
                {
                    Id = Guid.NewGuid(),
                    Category = Category.Instrument,
                    Name = "Pojacalo"
                },
                Reviews = new List<Review>(),
            });*/
        }
    }
}
