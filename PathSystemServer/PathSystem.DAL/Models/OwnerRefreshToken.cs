namespace PathSystem.DAL.Models
{
    public class OwnerRefreshToken
    {
        public int Id { get; set; }
        public string Token { get; set; }
        public Owner Owner { get; set; }
    }
}
