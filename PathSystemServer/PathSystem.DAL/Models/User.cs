using System.Collections.Generic;

namespace PathSystem.DAL.Models
{
    public class User
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string PasswordHash { get; set; }
        public ICollection<UserRefreshToken> RefreshTokens { get; set; }
        public ICollection<UserRoute> UserRoutes { get; set; }
        public ICollection<UserPathPoint> UserPathPoints { get; set; }
    }
}
