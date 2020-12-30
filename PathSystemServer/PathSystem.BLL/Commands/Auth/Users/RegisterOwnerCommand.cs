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
    public class RegisterUserCommand : IRequest<UserDTO>
    {
        [Required]
        public string Name { get; set; }
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        [Required]
        public string Password { get; set; }
    }

    public class RegisterUserCommandHandler : IRequestHandler<RegisterUserCommand, UserDTO>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly TokenService _tokenService;

        public RegisterUserCommandHandler(IUnitOfWork unitOfWork, TokenService tokenService)
        {
            _unitOfWork = unitOfWork;
            _tokenService = tokenService;
        }
        public Task<UserDTO> Handle(RegisterUserCommand request, CancellationToken cancellationToken)
        {
            if (_unitOfWork.Users.GetAll(u => u.Email == request.Email).SingleOrDefault() != null)
                throw new AppException("Owner already exists");

            var user = new User
            {
                Name = request.Name,
                Email = request.Email,
                PasswordHash = Crypto.HashPassword(request.Password)
            };

            _unitOfWork.Users.Add(user);

            var accessToken = _tokenService.GenerateAccessToken(user);
            var refreshToken = _tokenService.GenerateRefreshToken();

            _unitOfWork.UserRefreshTokens.Add(new UserRefreshToken { Token = refreshToken, User = user });

            _unitOfWork.Commit();

            return Task.FromResult(new UserDTO(user.Id, user.Name, user.Email, accessToken, refreshToken));
        }
    }
}
