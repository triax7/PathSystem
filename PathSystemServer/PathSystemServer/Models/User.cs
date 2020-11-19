using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PathSystemServer.Models
{
    public class User
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string PasswordHash { get; set; }
        public ICollection<RefreshToken> RefreshTokens { get; set; }
        public ICollection<UserRoute> UserRoutes { get; set; }
        public ICollection<UserPathPoint> UserPathPoints { get; set; }
    }
}
