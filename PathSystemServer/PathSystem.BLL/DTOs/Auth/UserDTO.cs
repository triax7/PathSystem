namespace PathSystem.BLL.DTOs.Auth
{
    public class UserDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string AccessToken { get; set; }
        public string RefreshToken { get; set; }

        public UserDTO(int id, string name, string email, string accessToken, string refreshToken)
        {
            Id = id;
            Name = name;
            Email = email;
            AccessToken = accessToken;
            RefreshToken = refreshToken;
        }

        public UserDTO(int id, string name, string email)
        {
            Id = id;
            Name = name;
            Email = email;
        }
    }
}
