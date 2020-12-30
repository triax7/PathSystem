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

namespace PathSystem.BLL.Commands.Auth.Users
{
    public class LoginUserCommand : IRequest<UserDTO>
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        [Required]
        public string Password { get; set; }
    }

    public class LoginUserCommandHandler : IRequestHandler<LoginUserCommand, UserDTO>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly TokenService _tokenService;

        public LoginUserCommandHandler(IUnitOfWork unitOfWork, TokenService tokenService)
        {
            _unitOfWork = unitOfWork;
            _tokenService = tokenService;
        }
        public Task<UserDTO> Handle(LoginUserCommand request, CancellationToken cancellationToken)
        {
            var user = _unitOfWork.Users.GetAll().SingleOrDefault(u => u.Email == request.Email);

            if (user == null || !Crypto.VerifyHashedPassword(user.PasswordHash, request.Password))
                throw new AppException("User not found");

            var accessToken = _tokenService.GenerateAccessToken(user);
            var refreshToken = _tokenService.GenerateRefreshToken();

            _unitOfWork.UserRefreshTokens.Add(new UserRefreshToken { Token = refreshToken, User = user });

            _unitOfWork.Commit();

            return Task.FromResult(new UserDTO(user.Id, user.Name, user.Email, accessToken, refreshToken));
        }
    }
}
