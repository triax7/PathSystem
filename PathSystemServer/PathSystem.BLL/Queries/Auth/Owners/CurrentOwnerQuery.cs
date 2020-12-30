using System.Net;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using PathSystem.BLL.Abstractions;
using PathSystem.BLL.DTOs.Auth;
using PathSystem.BLL.Exceptions;
using PathSystem.DAL.Repositories;

namespace PathSystem.BLL.Queries.Auth.Owners
{
    public class CurrentOwnerQuery : AuthorizedRequest, IRequest<UserDTO> { }

    public class CurrentOwnerQueryHandler : IRequestHandler<CurrentOwnerQuery, UserDTO>
    {
        private readonly IUnitOfWork _unitOfWork;

        public CurrentOwnerQueryHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public Task<UserDTO> Handle(CurrentOwnerQuery request, CancellationToken cancellationToken)
        {
            var owner = _unitOfWork.Owners.GetById(request.UserId);
            if (owner == null) throw new AppException("Owner not found", HttpStatusCode.NotFound);

            return Task.FromResult(new UserDTO(owner.Id, owner.Name, owner.Email));
        }
    }
}