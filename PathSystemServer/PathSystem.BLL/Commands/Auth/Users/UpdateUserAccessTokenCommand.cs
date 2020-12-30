using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using PathSystem.BLL.DTOs.Auth;
using PathSystem.BLL.Exceptions;
using PathSystem.BLL.Services;
using PathSystem.DAL.Models;
using PathSystem.DAL.Repositories;

namespace PathSystem.BLL.Commands.Auth.Users
{
    public class UpdateUserAccessTokenCommand : IRequest<UserDTO>
    {
        public string AccessToken { get; set; }
        public string RefreshToken { get; set; }

        public UpdateUserAccessTokenCommand(string accessToken, string refreshToken)
        {
            AccessToken = accessToken;
            RefreshToken = refreshToken;
        }
    }

    public class UpdateUserAccessTokenCommandHandler : IRequestHandler<UpdateUserAccessTokenCommand, UserDTO>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly TokenService _tokenService;

        public UpdateUserAccessTokenCommandHandler(IUnitOfWork unitOfWork, TokenService tokenService)
        {
            _unitOfWork = unitOfWork;
            _tokenService = tokenService;
        }
        public Task<UserDTO> Handle(UpdateUserAccessTokenCommand request, CancellationToken cancellationToken)
        {
            var accessToken = new JwtSecurityTokenHandler().ReadToken(request.AccessToken) as JwtSecurityToken;
            if (accessToken == null)
                throw new AppException("Invalid access token", HttpStatusCode.Unauthorized);
            
            var userId = Convert.ToInt32(accessToken.Claims.FirstOrDefault(c => c.Type == "nameid")?.Value);
            var user = _unitOfWork.Users.GetById(userId);

            if (user == null)
                throw new AppException("User not found", HttpStatusCode.NotFound);

            if (!_unitOfWork.UserRefreshTokens.GetAll().Any(t => t.Token == request.RefreshToken))
                throw new AppException("Invalid refresh token", HttpStatusCode.Unauthorized);

            var newAccessToken = _tokenService.GenerateAccessToken(user);
            var newRefreshToken = _tokenService.GenerateRefreshToken();

            _unitOfWork.UserRefreshTokens.Add(new UserRefreshToken { Token = newRefreshToken, User = user });

            _unitOfWork.Commit();

            return Task.FromResult(new UserDTO(user.Id, user.Name, user.Email, newAccessToken, newRefreshToken));
        }
    }
}
