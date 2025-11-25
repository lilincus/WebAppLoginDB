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

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Seed a test user: username "admin", password "password" (hashed)
            modelBuilder.Entity<User>().HasData(
                new User
                {
                    Id = 1,
                    Username = "admin",
                    PasswordHash = BCrypt.Net.BCrypt.HashPassword("password") // ✅ BCrypt works now
                }
            );
        }
    }
}
