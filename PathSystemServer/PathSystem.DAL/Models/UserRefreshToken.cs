namespace PathSystem.DAL.Models
{
    public class UserRefreshToken
    {
        public int Id { get; set; }
        public string Token { get; set; }
        public User User { get; set; }
    }
}
