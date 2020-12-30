using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using PathSystem.BLL.Abstractions;
using PathSystem.BLL.DTOs.Routing;
using PathSystem.DAL.Models;
using PathSystem.DAL.Repositories;

namespace PathSystem.BLL.Commands.Routes
{
    public class CreateRouteCommand : AuthorizedRequest, IRequest<RouteDTO>
    {
        public string Name { get; set; }

        public CreateRouteCommand(string name)
        {
            Name = name;
        }
    }
    
    public class CreateRouteCommandHandler : IRequestHandler<CreateRouteCommand, RouteDTO>
    {
        private readonly IUnitOfWork _unitOfWork;

        public CreateRouteCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public Task<RouteDTO> Handle(CreateRouteCommand request, CancellationToken cancellationToken)
        {
            Route route = new Route { Name = request.Name, OwnerId = request.UserId};
            _unitOfWork.Routes.Add(route);

            _unitOfWork.Commit();

            return Task.FromResult(new RouteDTO(route.Id, route.Name));
        }
    }
}
