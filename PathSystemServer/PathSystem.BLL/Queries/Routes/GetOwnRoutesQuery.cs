using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using PathSystem.BLL.Abstractions;
using PathSystem.BLL.DTOs.Routing;
using PathSystem.DAL.Repositories;

namespace PathSystem.BLL.Queries.Routes
{
    public class GetOwnRoutesQuery : AuthorizedRequest, IRequest<IEnumerable<RouteDTO>> { }

    public class GetOwnRoutesQueryHandler : IRequestHandler<GetOwnRoutesQuery, IEnumerable<RouteDTO>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public GetOwnRoutesQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public Task<IEnumerable<RouteDTO>> Handle(GetOwnRoutesQuery request, CancellationToken cancellationToken)
        {
            var routes = _unitOfWork.Routes.GetAll(r => r.Owner.Id == request.UserId).ToList();
            return Task.FromResult(_mapper.Map<IEnumerable<RouteDTO>>(routes));
        }
    }
}
