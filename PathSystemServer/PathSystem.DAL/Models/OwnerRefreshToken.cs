using PathSystem.DAL.Abstractions;

namespace PathSystem.DAL.Models
{
    public class OwnerRefreshToken : BaseEntity
    {
        public string Token { get; set; }
        public Owner Owner { get; set; }
    }
}
