using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.EntityFrameworkCore;
using PathSystem.BLL.Abstractions;
using PathSystem.BLL.Exceptions;
using PathSystem.DAL.Repositories;

namespace PathSystem.BLL.Commands.PathPoints
{
    public class DeletePathPointCommand : AuthorizedRequest, IRequest<bool>
    {
        public int PathPointId { get; set; }

        public DeletePathPointCommand(int pathPointId)
        {
            PathPointId = pathPointId;
        }
    }

    public class DeletePathPointCommandHandler : IRequestHandler<DeletePathPointCommand, bool>
    {
        private readonly IUnitOfWork _unitOfWork;

        public DeletePathPointCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public Task<bool> Handle(DeletePathPointCommand request, CancellationToken cancellationToken)
        {
            var point = _unitOfWork.PathPoints.GetAll().Include(p => p.Route.Owner)
                .SingleOrDefault(p => p.Id == request.PathPointId);
            
            if (point == null || point.Route.Owner.Id != request.UserId)
                throw new AppException("Point does not exist or does not belong to you");
            
            _unitOfWork.PathPoints.Delete(point);
            _unitOfWork.Commit();

            return Task.FromResult(true);
        }
    }
}