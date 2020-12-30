using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using PathSystem.DAL.Repositories;

namespace PathSystem.BLL.Commands.Auth.Users
{
    public class RevokeUserRefreshTokenCommand : IRequest<bool>
    {
        public string RefreshToken { get; set; }

        public RevokeUserRefreshTokenCommand(string refreshToken)
        {
            RefreshToken = refreshToken;
        }
    }

    public class RevokeUserRefreshTokenCommandHandler : IRequestHandler<RevokeUserRefreshTokenCommand, bool>
    {
        private readonly IUnitOfWork _unitOfWork;

        public RevokeUserRefreshTokenCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public Task<bool> Handle(RevokeUserRefreshTokenCommand request, CancellationToken cancellationToken)
        {
            var token = _unitOfWork.UserRefreshTokens.GetAll().FirstOrDefault(t => t.Token == request.RefreshToken);
            if (token == null) return Task.FromResult(false);

            _unitOfWork.UserRefreshTokens.Delete(token);

            _unitOfWork.Commit();

            return Task.FromResult(true);
        }
    }



}
