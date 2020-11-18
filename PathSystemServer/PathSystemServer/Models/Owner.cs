using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PathSystemServer.Models
{
    public class Owner
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string PasswordHash { get; set; }
        public ICollection<RefreshToken> RefreshTokens { get; set; }
        public Role Role { get; set; }
    }

    public enum Role
    {
        User = 1,
        Admin = 2
    }
}
