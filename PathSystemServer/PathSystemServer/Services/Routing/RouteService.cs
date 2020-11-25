using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using PathSystemServer.DTOs.Routing;
using PathSystemServer.Models;
using PathSystemServer.Repository.UnitOfWork;
using PathSystemServer.Services.Auth;

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

        public RouteDTO CreateRoute(RouteDTO dto, JwtSecurityToken token)
        {
            var owner = _ownerService.GetUserFromToken(token);
            if (owner == null) throw new ApplicationException("Invalid token");

            Route route = new Route {Name = dto.Name, Owner = owner};
            _unitOfWork.Routes.Add(route);

            _unitOfWork.Commit();

            dto.Id = route.Id;

            return dto;
        }

        public List<RouteDTO> GetOwnRoutes(JwtSecurityToken token)
        {
            var owner = _ownerService.GetUserFromToken(token);
            if (owner == null) return null;
            var routes = _unitOfWork.Routes.GetAll(r => r.Owner.Id == owner.Id).ToList();

            return _mapper.Map<List<RouteDTO>>(routes);
        }

        public RouteDTO GetRouteById(int id)
        {
            var route = _unitOfWork.Routes.GetAll(r => r.Id == id).SingleOrDefault();
            if (route == null) throw new ApplicationException("Route does not exist");

            return _mapper.Map<RouteDTO>(route);
        }

        public List<PathPointDTO> GetPointsByRouteId(int id)
        {
            var points = _unitOfWork.PathPoints.GetAll(p => p.Route.Id == id);

            return _mapper.Map<List<PathPointDTO>>(points);
        }
    }
}