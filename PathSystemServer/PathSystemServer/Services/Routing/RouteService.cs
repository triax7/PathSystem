using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using PathSystemServer.DTOs.Routing;
using PathSystemServer.ErrorHandling.Exceptions;
using PathSystemServer.Models;
using PathSystemServer.Repository.UnitOfWork;
using PathSystemServer.Services.Auth;
using PathSystemServer.ViewModels.Routing;

namespace PathSystemServer.Services.Routing
{
    public class RouteService : IRouteService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IOwnerService _ownerService;
        private readonly IMapper _mapper;

        public RouteService(IUnitOfWork unitOfWork, IOwnerService ownerService, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _ownerService = ownerService;
            _mapper = mapper;
        }

        public Route CreateRoute(RouteCreateViewModel model, JwtSecurityToken token)
        {
            var owner = _ownerService.GetUserFromToken(token);

            Route route = new Route {Name = model.Name, Owner = owner};
            _unitOfWork.Routes.Add(route);

            _unitOfWork.Commit();

            return route;
        }

        public List<Route> GetOwnRoutes(JwtSecurityToken token)
        {
            var owner = _ownerService.GetUserFromToken(token);

            var routes = _unitOfWork.Routes.GetAll(r => r.Owner.Id == owner.Id).ToList();

            return routes;
        }

        public Route GetRouteById(int id)
        {
            var route = _unitOfWork.Routes.GetById(id);
            if (route == null) throw new AppException("Route does not exist");

            return route;
        }

        public List<PathPointDTO> GetPointsByRouteId(int id)
        {
            var points = _unitOfWork.PathPoints.GetAll(p => p.Route.Id == id);

            return _mapper.Map<List<PathPointDTO>>(points);
        }

        public List<PathPointDTO> GetOptimizedRoute(int id, int startingPointId)
        {
            var points = _unitOfWork.PathPoints.GetAll(p => p.Route.Id == id).ToList();
            var startingPoint = points.SingleOrDefault(p => p.Id == startingPointId);

            if (_unitOfWork.Routes.GetById(id) == null)
                throw new AppException("Route does not exist");

            if (startingPoint == null)
                throw new AppException("Point does not belong to this route or does not exist");

            var result = CalculateNearestNeighborPath(points, startingPoint);

            return _mapper.Map<List<PathPointDTO>>(result);
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