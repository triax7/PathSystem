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
using PathSystemServer.ErrorHandling.Exceptions;
using PathSystemServer.Models;
using PathSystemServer.Repository;
using PathSystemServer.Repository.Interfaces;
using PathSystemServer.Repository.UnitOfWork;

namespace PathSystemServer.Services.Auth
{
    public class OwnerService : IOwnerService
    {
        private readonly JwtOptions _jwtOptions;
        private readonly IUnitOfWork _unitOfWork;

        public OwnerService(IOptions<JwtOptions> jwtOptionsMonitor, IUnitOfWork unitOfWork)
        {
            _jwtOptions = jwtOptionsMonitor.Value;
            _unitOfWork = unitOfWork;
        }

        public LoginSuccessDTO Login(LoginDTO dto)
        {
            var owner = _unitOfWork.Owners.GetAll().SingleOrDefault(u => u.Email == dto.Email);

            if (owner == null || !Crypto.VerifyHashedPassword(owner.PasswordHash, dto.Password))
                throw new AppException("Owner not found");

            var accessToken = GenerateAccessToken(owner);
            var refreshToken = GenerateRefreshToken();

            _unitOfWork.OwnerRefreshTokens.Add(new OwnerRefreshToken {Token = refreshToken, Owner = owner});

            _unitOfWork.Commit();

            return new LoginSuccessDTO(owner.Name, accessToken, refreshToken);
        }

        public LoginSuccessDTO Register(RegisterDTO dto)
        {
            if (_unitOfWork.Owners.GetAll(u => u.Email == dto.Email).SingleOrDefault() != null)
                throw new AppException("Owner already exists");

            var owner = new Owner
            {
                Name = dto.Name,
                Email = dto.Email,
                PasswordHash = Crypto.HashPassword(dto.Password)
            };

            _unitOfWork.Owners.Add(owner);

            var accessToken = GenerateAccessToken(owner);
            var refreshToken = GenerateRefreshToken();

            _unitOfWork.OwnerRefreshTokens.Add(new OwnerRefreshToken {Token = refreshToken, Owner = owner});

            _unitOfWork.Commit();

            return new LoginSuccessDTO(owner.Name, accessToken, refreshToken);
        }

        public LoginSuccessDTO UpdateAccessToken(JwtSecurityToken accessToken, string refreshToken)
        {
            var owner = GetUserFromToken(accessToken);

            if (owner == null)
                throw new AppException("Owner not found");

            if (!_unitOfWork.OwnerRefreshTokens.GetAll().Any(t => t.Token == refreshToken))
                throw new AppException("Invalid refresh token");

            var newAccessToken = GenerateAccessToken(owner);
            var newRefreshToken = GenerateRefreshToken();

            _unitOfWork.OwnerRefreshTokens.Add(new OwnerRefreshToken {Token = newRefreshToken, Owner = owner});

            _unitOfWork.Commit();

            return new LoginSuccessDTO(owner.Name, newAccessToken, newRefreshToken);
        }

        public void RevokeRefreshToken(string refreshToken)
        {
            var token = _unitOfWork.OwnerRefreshTokens.GetAll().FirstOrDefault(t => t.Token == refreshToken);
            _unitOfWork.OwnerRefreshTokens.Delete(token);

            _unitOfWork.Commit();
        }

        public bool EmailExists(string email)
        {
            return _unitOfWork.Owners.GetAll(o => o.Email == email).SingleOrDefault() != null;
        }

        private string GenerateAccessToken(Owner owner)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_jwtOptions.SecretKey);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Name, owner.Name),
                    new Claim(ClaimTypes.Email, owner.Email),
                    new Claim(ClaimTypes.Role, "Owner")
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

        public Owner GetUserFromToken(JwtSecurityToken accessToken)
        {
            var email = accessToken.Claims
                .FirstOrDefault(c => c.Type == "email")?.Value;
            var owner = _unitOfWork.Owners
                .GetAll(u => u.Email == email)
                .FirstOrDefault();
            return owner;
        }
    }
}