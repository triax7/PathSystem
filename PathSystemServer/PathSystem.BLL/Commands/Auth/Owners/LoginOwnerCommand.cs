using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Helpers;
using MediatR;
using PathSystem.BLL.DTOs.Auth;
using PathSystem.BLL.Exceptions;
using PathSystem.BLL.Services;
using PathSystem.DAL.Models;
using PathSystem.DAL.Repositories;

namespace PathSystem.BLL.Commands.Auth.Owners
{
    public class LoginOwnerCommand : IRequest<UserDTO>
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        [Required]
        public string Password { get; set; }
    }

    public class LoginOwnerCommandHandler : IRequestHandler<LoginOwnerCommand, UserDTO>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly TokenService _tokenService;

        public LoginOwnerCommandHandler(IUnitOfWork unitOfWork, TokenService tokenService)
        {
            _unitOfWork = unitOfWork;
            _tokenService = tokenService;
        }
        public Task<UserDTO> Handle(LoginOwnerCommand request, CancellationToken cancellationToken)
        {
            var owner = _unitOfWork.Owners.GetAll().SingleOrDefault(u => u.Email == request.Email);

            if (owner == null || !Crypto.VerifyHashedPassword(owner.PasswordHash, request.Password))
                throw new AppException("Owner not found");

            var accessToken = _tokenService.GenerateAccessToken(owner);
            var refreshToken = _tokenService.GenerateRefreshToken();

            _unitOfWork.OwnerRefreshTokens.Add(new OwnerRefreshToken { Token = refreshToken, Owner = owner });

            _unitOfWork.Commit();

            return Task.FromResult(new UserDTO(owner.Id, owner.Name, owner.Email, accessToken, refreshToken));
        }
    }
}
