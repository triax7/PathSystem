using Microsoft.EntityFrameworkCore;
using PathSystem.DAL.Models;

namespace PathSystem.DAL
{
    public class ApplicationContext : DbContext
    {
        public ApplicationContext(DbContextOptions<ApplicationContext> options) : base(options)
        {
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
