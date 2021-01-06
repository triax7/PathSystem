using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using NetTopologySuite.Geometries;
using PathSystem.BLL.Abstractions;
using PathSystem.BLL.DTOs.Routing;
using PathSystem.BLL.Exceptions;
using PathSystem.BLL.Queries.Auth.Owners;
using PathSystem.DAL.Models;
using PathSystem.DAL.Repositories;

namespace PathSystem.BLL.Commands.PathPoints
{
    public class CreatePathPointCommand : AuthorizedRequest, IRequest<PathPointDTO>
    {
        [Required] public int RouteId { get; set; }
        [Required] public string Name { get; set; }
        [Required] public double Longitude { get; set; }
        [Required] public double Latitude { get; set; }
    }

    public class CreatePathPointCommandHandler : IRequestHandler<CreatePathPointCommand, PathPointDTO>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public CreatePathPointCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public Task<PathPointDTO> Handle(CreatePathPointCommand request, CancellationToken cancellationToken)
        {
            var route = _unitOfWork.Routes.GetById(request.RouteId);
            var ownsRoute = _unitOfWork.Owners.GetByIdWithRoutes(request.UserId).Routes.Contains(route);
            if (route == null || !ownsRoute)
                throw new AppException("Route does not exist", HttpStatusCode.NotFound);

            var point = new PathPoint
            {
                Name = request.Name,
                Point = new Point(request.Longitude, request.Latitude) {SRID = 4326},
                Route = route
            };
            _unitOfWork.PathPoints.Add(point);
            _unitOfWork.Commit();

            return Task.FromResult(_mapper.Map<PathPointDTO>(point));
        }
    }
}