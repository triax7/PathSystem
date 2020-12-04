using System.IdentityModel.Tokens.Jwt;
using PathSystemServer.DTOs.Auth;
using PathSystemServer.Models;

namespace PathSystemServer.Services.Auth
{
    public interface IUserService
    {
        LoginSuccessDTO Login(LoginDTO dto);
        LoginSuccessDTO Register(RegisterDTO dto);
        LoginSuccessDTO UpdateAccessToken(JwtSecurityToken accessToken, string refreshToken);
        void RevokeRefreshToken(string refreshToken);
        User GetUserFromToken(JwtSecurityToken accessToken);
        bool EmailExists(string email);

    }
}