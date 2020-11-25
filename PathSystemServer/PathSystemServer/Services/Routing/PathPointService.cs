using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using NetTopologySuite.Geometries;
using PathSystemServer.DTOs.Routing;
using PathSystemServer.Models;
using PathSystemServer.Repository.UnitOfWork;
using PathSystemServer.Services.Auth;

namespace PathSystemServer.Services.Routing
{
    public class PathPointService : IPathPointService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IOwnerService _ownerService;
        private readonly IMapper _mapper;

        public PathPointService(IUnitOfWork unitOfWork, IOwnerService ownerService, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _ownerService = ownerService;
            _mapper = mapper;
        }

        public PathPointDTO AddPointToRoute(int routeId, PathPointDTO dto, JwtSecurityToken token)
        {
            var route = _unitOfWork.Routes.GetById(routeId);
            if (route == null) throw new ApplicationException("Route does not exist");

            var owner = _ownerService.GetUserFromToken(token);
            if (owner == null) throw new ApplicationException("Invalid token");

            if (route.Owner.Id != owner.Id) throw new ApplicationException("You are not the owner of this route");

            var point = new PathPoint
            {
                Name = dto.Name,
                Point = new Point(dto.Longitude, dto.Latitude) {SRID = 4326},
                Route = route
            };
            _unitOfWork.PathPoints.Add(point);
            _unitOfWork.Commit();

            dto.Id = point.Id;
            return dto;
        }
    }
}