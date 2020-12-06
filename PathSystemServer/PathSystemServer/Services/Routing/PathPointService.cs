using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Threading.Tasks;
using System.Xml.Serialization;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using NetTopologySuite.Geometries;
using PathSystemServer.DTOs.Routing;
using PathSystemServer.ErrorHandling.Exceptions;
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
            if (route == null) throw new AppException("Route does not exist");

            var owner = _ownerService.GetUserFromToken(token);

            if (route.Owner.Id != owner.Id) throw new AppException("You are not the owner of this route");

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

        public void DeletePoint(int pointId, JwtSecurityToken token)
        {
            var owner = _ownerService.GetUserFromToken(token);
            var point = _unitOfWork.PathPoints.GetAll(p => p.Id == pointId).Include(p => p.Route).SingleOrDefault();
            if(point == null || point.Route.Owner.Id != owner.Id) throw new AppException("Point does not exist");
            
            _unitOfWork.PathPoints.Delete(point);
            _unitOfWork.Commit();
        }
    }
}