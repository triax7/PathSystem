using System.IdentityModel.Tokens.Jwt;
using PathSystemServer.DTOs.Auth;
using PathSystemServer.Models;
using PathSystemServer.ViewModels.Auth;

namespace PathSystemServer.Services.Auth
{
    public interface IOwnerService
    {
        UserDTO Login(LoginViewModel model);
        UserDTO Register(RegisterViewModel model);
        UserDTO UpdateAccessToken(JwtSecurityToken accessToken, string refreshToken);
        void RevokeRefreshToken(string refreshToken);
        Owner GetUserFromToken(JwtSecurityToken accessToken);
        bool EmailExists(string email);
    }
}