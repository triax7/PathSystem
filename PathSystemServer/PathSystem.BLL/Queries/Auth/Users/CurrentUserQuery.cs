using System.Net;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using PathSystem.BLL.Abstractions;
using PathSystem.BLL.DTOs.Auth;
using PathSystem.BLL.Exceptions;
using PathSystem.DAL.Repositories;

namespace PathSystem.BLL.Queries.Auth.Users
{
    public class CurrentUserQuery : AuthorizedRequest, IRequest<UserDTO>
    {
    }

    public class CurrentUserQueryHandler : IRequestHandler<CurrentUserQuery, UserDTO>
    {
        private readonly IUnitOfWork _unitOfWork;

        public CurrentUserQueryHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public Task<UserDTO> Handle(CurrentUserQuery request, CancellationToken cancellationToken)
        {
            var user = _unitOfWork.Users.GetById(request.UserId);
            if (user == null) throw new AppException("User not found", HttpStatusCode.NotFound);

            return Task.FromResult(new UserDTO(user.Id, user.Name, user.Email));
        }
    }
}