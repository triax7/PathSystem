using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
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
            var point = _unitOfWork.PathPoints.GetByIdWithRoute(request.PathPointId);
            var ownsRoute = _unitOfWork.Owners.GetByIdWithRoutes(request.UserId).Routes.Contains(point.Route);
            
            if (point == null || !ownsRoute)
                throw new AppException("Point does not exist", HttpStatusCode.NotFound);
            
            _unitOfWork.PathPoints.Delete(point);
            _unitOfWork.Commit();

            return Task.FromResult(true);
        }
    }
}