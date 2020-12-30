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
    public class RegisterOwnerCommand : IRequest<UserDTO>
    {
        [Required]
        public string Name { get; set; }
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        [Required]
        public string Password { get; set; }
    }

    public class RegisterOwnerCommandHandler : IRequestHandler<RegisterOwnerCommand, UserDTO>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly TokenService _tokenService;

        public RegisterOwnerCommandHandler(IUnitOfWork unitOfWork, TokenService tokenService)
        {
            _unitOfWork = unitOfWork;
            _tokenService = tokenService;
        }
        public Task<UserDTO> Handle(RegisterOwnerCommand request, CancellationToken cancellationToken)
        {
            if (_unitOfWork.Owners.GetAll(u => u.Email == request.Email).SingleOrDefault() != null)
                throw new AppException("Owner already exists");

            var owner = new Owner
            {
                Name = request.Name,
                Email = request.Email,
                PasswordHash = Crypto.HashPassword(request.Password)
            };

            _unitOfWork.Owners.Add(owner);

            var accessToken = _tokenService.GenerateAccessToken(owner);
            var refreshToken = _tokenService.GenerateRefreshToken();

            _unitOfWork.OwnerRefreshTokens.Add(new OwnerRefreshToken { Token = refreshToken, Owner = owner });

            _unitOfWork.Commit();

            return Task.FromResult(new UserDTO(owner.Id, owner.Name, owner.Email, accessToken, refreshToken));
        }
    }
}
