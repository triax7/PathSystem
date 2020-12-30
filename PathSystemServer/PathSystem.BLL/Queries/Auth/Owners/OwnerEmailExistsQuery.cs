using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using PathSystem.DAL.Repositories;

namespace PathSystem.BLL.Queries.Auth.Owners
{
    public class OwnerEmailExistsQuery : IRequest<bool>
    {
        public string Email { get; set; }

        public OwnerEmailExistsQuery(string email)
        {
            Email = email;
        }
    }

    public class OwnerEmailExistsQueryHandler : IRequestHandler<OwnerEmailExistsQuery, bool>
    {
        private readonly IUnitOfWork _unitOfWork;

        public OwnerEmailExistsQueryHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public Task<bool> Handle(OwnerEmailExistsQuery request, CancellationToken cancellationToken)
        {
            return Task.FromResult(_unitOfWork.Owners.GetAll(o => o.Email == request.Email).SingleOrDefault() != null);
        }
    }
}