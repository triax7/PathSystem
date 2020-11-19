using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Helpers;
using Microsoft.EntityFrameworkCore;
using PathSystemServer.Models;

namespace PathSystemServer
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
                    PasswordHash = Crypto.HashPassword("admin")
                }
            });
        }
        public DbSet<User> Users { get; set; }
        public DbSet<RefreshToken> RefreshTokens { get; set; }
        public DbSet<Owner> Owners { get; set; }
        public DbSet<Route> Routes { get; set; }
        public DbSet<PathPoint> PathPoints { get; set; }
        public DbSet<UserPathPoint> UserPathPoints { get; set; }
    }
}
