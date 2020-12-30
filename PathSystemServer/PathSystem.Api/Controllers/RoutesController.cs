using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using PathSystem.Api.Extensions;
using PathSystem.BLL.Commands.Routes;
using PathSystem.BLL.DTOs.Routing;
using PathSystem.BLL.Queries.PathPoints;
using PathSystem.BLL.Queries.Routes;

namespace PathSystem.Api.Controllers
{
    [Microsoft.AspNetCore.Mvc.Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles="Owner")]
    public class RoutesController : ControllerBase
    {
        private readonly IMediator _mediator;

        public RoutesController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost("Create")]
        public async Task<ActionResult<RouteDTO>> Create([FromBody] CreateRouteCommand command)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return await _mediator.Send(command);
        }

        [HttpGet]
        public async Task<IEnumerable<RouteDTO>> Get()
        {
            return await _mediator.Send(new GetOwnRoutesQuery());
        }

        [HttpGet("get-points/{id}")]
        public async Task<IEnumerable<PathPointDTO>> GetPointsById([FromRoute] int id)
        {
            return await _mediator.Send(new GetPointsByRouteIdQuery(id));
        }

        [HttpPost("optimize")]
        public async Task<IEnumerable<PathPointDTO>> GetOptimizedRoute([FromBody] GetOptimizedRouteQuery query)
        {
            return await _mediator.Send(query);
        }
    }
}
