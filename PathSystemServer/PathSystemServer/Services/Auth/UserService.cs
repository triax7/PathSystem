using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Web.Helpers;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using PathSystemServer.DTOs.Auth;
using PathSystemServer.Models;
using PathSystemServer.Repository;
using PathSystemServer.Repository.Interfaces;
using PathSystemServer.Repository.UnitOfWork;

namespace PathSystemServer.Services.Auth
{
    public class UserService : IUserService
    {
        private readonly JwtOptions _jwtOptions;
        private readonly IUnitOfWork _unitOfWork;

        public UserService(IOptions<JwtOptions> jwtOptionsMonitor, IUnitOfWork unitOfWork)
        {
            _jwtOptions = jwtOptionsMonitor.Value;
            _unitOfWork = unitOfWork;
        }

        public LoginSuccessDTO Login(LoginDTO dto)
        {
            var user = _unitOfWork.Users.GetAll().FirstOrDefault(u => u.Email == dto.Email);

            if (user == null || !Crypto.VerifyHashedPassword(user.PasswordHash, dto.Password))
                throw new ApplicationException("User not found");

            var accessToken = GenerateAccessToken(user);
            var refreshToken = GenerateRefreshToken();

            _unitOfWork.RefreshTokens.Add(new RefreshToken { Token = refreshToken, User = user });

            _unitOfWork.Commit();

            return new LoginSuccessDTO(user.Name, accessToken, refreshToken);
        }

        public LoginSuccessDTO Register(RegisterDTO dto)
        {
            if (_unitOfWork.Users.GetAll(u => u.Email == dto.Email) != null)
                throw new ApplicationException("User not found");

            var user = new User
            {
                Email = dto.Email,
                PasswordHash = Crypto.HashPassword(dto.Password),
            };

            _unitOfWork.Users.Add(user);

            var accessToken = GenerateAccessToken(user);
            var refreshToken = GenerateRefreshToken();

            _unitOfWork.RefreshTokens.Add(new RefreshToken{Token = refreshToken, User = user});

            _unitOfWork.Commit();

            return new LoginSuccessDTO(user.Name, accessToken, refreshToken);
        }

        public LoginSuccessDTO UpdateAccessToken(JwtSecurityToken accessToken, string refreshToken)
        {
            var user = GetUserFromToken(accessToken);

            if (user == null)
            {
                throw new ApplicationException("User not found");
            }

            var newAccessToken = GenerateAccessToken(user);
            var newRefreshToken = GenerateRefreshToken();

            return new LoginSuccessDTO(user.Name, newAccessToken, newRefreshToken);
        }

        public void RevokeRefreshToken(string refreshToken)
        {
            var token = _unitOfWork.RefreshTokens.GetAll().FirstOrDefault(t => t.Token == refreshToken);
            _unitOfWork.RefreshTokens.Delete(token);

            _unitOfWork.Commit();
        }
        private string GenerateAccessToken(User user)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_jwtOptions.SecretKey);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Name, user.Name),
                    new Claim(ClaimTypes.Email, user.Email),
                }),
                Expires = DateTime.UtcNow.AddSeconds(_jwtOptions.ExpiryTime),
                SigningCredentials =
                    new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature),
                Audience = _jwtOptions.Audience,
                Issuer = _jwtOptions.Issuer
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }

        private string GenerateRefreshToken()
        {
            using var rngCryptoServiceProvider = new RNGCryptoServiceProvider();
            var randomBytes = new byte[64];
            rngCryptoServiceProvider.GetBytes(randomBytes);
            return Convert.ToBase64String(randomBytes);
        }

        private User GetUserFromToken(JwtSecurityToken accessToken)
        {
            var email = accessToken.Claims
                .FirstOrDefault(c => c.Type == "email")?.Value;
            var user = _unitOfWork.Users
                .GetAll(u => u.Email == email)
                .FirstOrDefault();
            return user;
        }
    }
}
