using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using PathSystem.BLL.DTOs.Auth;
using PathSystem.BLL.Exceptions;
using PathSystem.BLL.Services;
using PathSystem.DAL.Models;
using PathSystem.DAL.Repositories;

namespace PathSystem.BLL.Commands.Auth.Owners
{
    public class UpdateOwnerAccessTokenCommand : IRequest<UserDTO>
    {
        public string AccessToken { get; set; }
        public string RefreshToken { get; set; }

        public UpdateOwnerAccessTokenCommand(string accessToken, string refreshToken)
        {
            AccessToken = accessToken;
            RefreshToken = refreshToken;
        }
    }

    public class UpdateOwnerAccessTokenCommandHandler : IRequestHandler<UpdateOwnerAccessTokenCommand, UserDTO>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly TokenService _tokenService;

        public UpdateOwnerAccessTokenCommandHandler(IUnitOfWork unitOfWork, TokenService tokenService)
        {
            _unitOfWork = unitOfWork;
            _tokenService = tokenService;
        }
        public Task<UserDTO> Handle(UpdateOwnerAccessTokenCommand request, CancellationToken cancellationToken)
        {
            var accessToken = new JwtSecurityTokenHandler().ReadToken(request.AccessToken) as JwtSecurityToken;
            if (accessToken == null)
                throw new AppException("Invalid access token");
            
            var userId = Convert.ToInt32(accessToken.Claims.FirstOrDefault(c => c.Type == "nameid")?.Value);
            var owner = _unitOfWork.Owners.GetById(userId);

            if (owner == null)
                throw new AppException("Owner not found");

            if (!_unitOfWork.OwnerRefreshTokens.GetAll().Any(t => t.Token == request.RefreshToken))
                throw new AppException("Invalid refresh token");

            var newAccessToken = _tokenService.GenerateAccessToken(owner);
            var newRefreshToken = _tokenService.GenerateRefreshToken();

            _unitOfWork.OwnerRefreshTokens.Add(new OwnerRefreshToken { Token = newRefreshToken, Owner = owner });

            _unitOfWork.Commit();

            return Task.FromResult(new UserDTO(owner.Id, owner.Name, owner.Email, newAccessToken, newRefreshToken));
        }
    }
}
