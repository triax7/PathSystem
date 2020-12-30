using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using PathSystem.DAL.Repositories;

namespace PathSystem.BLL.Commands.Auth.Owners
{
    public class RevokeOwnerRefreshTokenCommand : IRequest<bool>
    {
        public string RefreshToken { get; set; }

        public RevokeOwnerRefreshTokenCommand(string refreshToken)
        {
            RefreshToken = refreshToken;
        }
    }

    public class RevokeOwnerRefreshTokenCommandHandler : IRequestHandler<RevokeOwnerRefreshTokenCommand, bool>
    {
        private readonly IUnitOfWork _unitOfWork;

        public RevokeOwnerRefreshTokenCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public Task<bool> Handle(RevokeOwnerRefreshTokenCommand request, CancellationToken cancellationToken)
        {
            var token = _unitOfWork.OwnerRefreshTokens.GetAll().FirstOrDefault(t => t.Token == request.RefreshToken);
            if (token == null) return Task.FromResult(false);

            _unitOfWork.OwnerRefreshTokens.Delete(token);

            _unitOfWork.Commit();

            return Task.FromResult(true);
        }
    }



}
