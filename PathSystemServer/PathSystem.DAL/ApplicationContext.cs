using Microsoft.EntityFrameworkCore;
using PathSystem.DAL.Models;

namespace PathSystem.DAL
{
    public class ApplicationContext : DbContext
    {
        public ApplicationContext(DbContextOptions<ApplicationContext> options) : base(options)
        {
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<User>().HasData(new[] {
                new User()
                {
                    Id = 1,
                    Name = "Admin",
                    Email = "admin@admin.admin",
                    PasswordHash = BCrypt.Net.BCrypt.HashPassword("admin")
                }
            });
        }
        public DbSet<User> Users { get; set; }
        public DbSet<UserRefreshToken> UserRefreshTokens { get; set; }
        public DbSet<Owner> Owners { get; set; }
        public DbSet<OwnerRefreshToken> OwnerRefreshTokens { get; set; }
        public DbSet<Route> Routes { get; set; }
        public DbSet<PathPoint> PathPoints { get; set; }
        public DbSet<UserPathPoint> UserPathPoints { get; set; }
    }
}
