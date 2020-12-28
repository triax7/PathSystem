using System.IdentityModel.Tokens.Jwt;
using PathSystemServer.DTOs.Auth;
using PathSystemServer.Models;

namespace PathSystemServer.Services.Auth
{
    public interface IUserService
    {
        UserDTO Login(LoginDTO dto);
        UserDTO Register(RegisterDTO dto);
        UserDTO UpdateAccessToken(JwtSecurityToken accessToken, string refreshToken);
        void RevokeRefreshToken(string refreshToken);
        User GetUserFromToken(JwtSecurityToken accessToken);
        bool EmailExists(string email);

    }
}