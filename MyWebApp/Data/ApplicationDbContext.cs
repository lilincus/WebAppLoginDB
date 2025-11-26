using Microsoft.EntityFrameworkCore;
using MyWebApp.Models;
using BCrypt.Net; // ✅ Add this to fix 'BCrypt' not found

namespace MyWebApp.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<User> Users { get; set; } = null!; // ✅ Initialize to remove nullable warnings
        public DbSet<Product> Products { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
{
    base.OnModelCreating(modelBuilder);

    modelBuilder.Entity<User>().HasData(
        new User
        {
            Id = 1,
            Username = "admin",
            PasswordHash = BCrypt.Net.BCrypt.HashPassword("password")
        },
        new User
        {
            Id = 2,
            Username = "joya",
            PasswordHash = BCrypt.Net.BCrypt.HashPassword("joyapass")
        },
        new User
        {
            Id = 3,
            Username = "mary",
            PasswordHash = BCrypt.Net.BCrypt.HashPassword("marypass")
        }
    );
    
    // Seed some products
    modelBuilder.Entity<Product>().HasData(
        new Product { Id = 1, Name = "Laptop", Price = 1200, Description = "Gaming Laptop" },
        new Product { Id = 2, Name = "Phone", Price = 800, Description = "Smartphone" }
    );
}
    }
}    
