using System.Collections.Generic;
using PathSystem.DAL.Abstractions;

namespace PathSystem.DAL.Models
{
    public class User : BaseEntity
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public string PasswordHash { get; set; }
        public ICollection<UserRefreshToken> RefreshTokens { get; set; }
        public ICollection<UserRoute> UserRoutes { get; set; }
        public ICollection<UserPathPoint> UserPathPoints { get; set; }
    }
}
