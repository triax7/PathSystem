using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using PathSystem.BLL.Abstractions;
using PathSystem.BLL.DTOs.Routing;
using PathSystem.BLL.Exceptions;
using PathSystem.DAL.Repositories;

namespace PathSystem.BLL.Queries.PathPoints
{
    public class GetPointsByRouteIdQuery : AuthorizedRequest, IRequest<IEnumerable<PathPointDTO>>
    {
        public int RouteId { get; set; }

        public GetPointsByRouteIdQuery(int routeId)
        {
            RouteId = routeId;
        }
    }

    public class GetPointsByRouteIdQueryHandler : IRequestHandler<GetPointsByRouteIdQuery, IEnumerable<PathPointDTO>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public GetPointsByRouteIdQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public Task<IEnumerable<PathPointDTO>> Handle(GetPointsByRouteIdQuery request,
            CancellationToken cancellationToken)
        {
            var route = _unitOfWork.Routes.GetAll().Include(r => r.PathPoints)
                .SingleOrDefault(r => r.Id == request.RouteId);

            if (route == null)
                throw new AppException("Route does not exist", HttpStatusCode.NotFound);

            return Task.FromResult(_mapper.Map<IEnumerable<PathPointDTO>>(route.PathPoints.ToList()));
        }
    }
}