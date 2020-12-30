using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using PathSystem.BLL.Commands.PathPoints;
using PathSystem.BLL.DTOs.Routing;

namespace PathSystem.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "Owner")]
    public class PathPointsController : ControllerBase
    {
        private readonly IMediator _mediator;

        public PathPointsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost("Create")]
        public async Task<ActionResult<PathPointDTO>> Create([FromBody] CreatePathPointCommand command)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return await _mediator.Send(command);
        }

        [HttpDelete("Delete/{id}")]
        public async Task<IActionResult> DeletePoint([FromRoute] int id)
        {
            await _mediator.Send(new DeletePathPointCommand(id));
            return Ok();
        }
    }
}
