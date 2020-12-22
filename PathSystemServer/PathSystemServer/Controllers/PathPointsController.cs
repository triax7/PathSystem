using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using PathSystemServer.DTOs.Routing;
using PathSystemServer.Services.Routing;
using PathSystemServer.ViewModels.Routing;

namespace PathSystemServer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "Owner")]
    public class PathPointsController : ControllerBase
    {
        private readonly IPathPointService _pathPointService;
        private readonly IMapper _mapper;

        public PathPointsController(IPathPointService pathPointService, IMapper mapper)
        {
            _pathPointService = pathPointService;
            _mapper = mapper;
        }

        [HttpPost("Create")]
        public ActionResult<PathPointViewModel> Create(AddPointViewModel model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var point = _pathPointService.AddPointToRoute(model.RouteId, _mapper.Map<PathPointDTO>(model),
                AccessToken());

            return _mapper.Map<PathPointViewModel>(point);
        }

        [HttpDelete("Delete/{id}")]
        public IActionResult DeletePoint([FromRoute] int id)
        {
            _pathPointService.DeletePoint(id, AccessToken());
            return Ok();
        }

        private JwtSecurityToken AccessToken()
        {
            var requestAccessToken = Request.Cookies["accessToken"];
            return new JwtSecurityTokenHandler().ReadToken(requestAccessToken) as JwtSecurityToken;
        }
    }
}
