using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using PathSystem.DAL.Repositories;

namespace PathSystem.BLL.Queries.Auth.Users
{
    public class UserEmailExistsQuery : IRequest<bool>
    {
        public string Email { get; set; }

        public UserEmailExistsQuery(string email)
        {
            Email = email;
        }
    }

    public class UserEmailExistsQueryHandler : IRequestHandler<UserEmailExistsQuery, bool>
    {
        private readonly IUnitOfWork _unitOfWork;

        public UserEmailExistsQueryHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public Task<bool> Handle(UserEmailExistsQuery request, CancellationToken cancellationToken)
        {
            return Task.FromResult(_unitOfWork.Users.GetAll(o => o.Email == request.Email).SingleOrDefault() != null);
        }
    }
}