using PathSystem.DAL.Abstractions;

namespace PathSystem.DAL.Models
{
    public class UserRefreshToken : BaseEntity
    {
        public string Token { get; set; }
        public User User { get; set; }
    }
}
