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
using PathSystem.BLL.Abstractions;
using PathSystem.BLL.DTOs.Routing;
using PathSystem.BLL.Exceptions;
using PathSystem.DAL.Models;
using PathSystem.DAL.Repositories;

namespace PathSystem.BLL.Queries.Routes
{
    public class GetOptimizedRouteQuery : AuthorizedRequest, IRequest<IEnumerable<PathPointDTO>>
    {
        [Required]
        public int RouteId { get; set; }
        [Required]
        public int StartingPointId { get; set; }
    }

    public class GetOptimizedRouteQueryHandler : IRequestHandler<GetOptimizedRouteQuery, IEnumerable<PathPointDTO>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public GetOptimizedRouteQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public Task<IEnumerable<PathPointDTO>> Handle(GetOptimizedRouteQuery request, CancellationToken cancellationToken)
        {
            if (_unitOfWork.Routes.GetById(request.RouteId) == null)
                throw new AppException("Route does not exist", HttpStatusCode.NotFound);

            var points = _unitOfWork.PathPoints.GetAll(p => p.Route.Id == request.RouteId).ToList();
            var startingPoint = points.SingleOrDefault(p => p.Id == request.StartingPointId);

            if (startingPoint == null)
                throw new AppException("Point does not exist", HttpStatusCode.NotFound);

            var result = CalculateNearestNeighborPath(points, startingPoint);

            return Task.FromResult(_mapper.Map<IEnumerable<PathPointDTO>>(result));
        }

        private List<PathPoint> CalculateNearestNeighborPath(List<PathPoint> points, PathPoint start)
        {
            List<PathPoint> path = new List<PathPoint>() { start };
            points.Remove(start);

            PathPoint current = start;

            while (points.Count != 0)
            {
                PathPoint nearestPoint = points[0];
                double minDistance = current.Point.Distance(nearestPoint.Point);

                foreach (PathPoint point in points)
                {
                    double distance = current.Point.Distance(point.Point);
                    if (distance < minDistance)
                    {
                        nearestPoint = point;
                        minDistance = distance;
                    }
                }

                path.Add(nearestPoint);
                points.Remove(nearestPoint);
                current = nearestPoint;
            }

            return path;
        }
    }
}
