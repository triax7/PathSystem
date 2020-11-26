using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Components;
using PathSystemServer.DTOs.Routing;
using PathSystemServer.Models;
using PathSystemServer.Services.Routing;
using PathSystemServer.ViewModels.Routing;

namespace PathSystemServer.Controllers
{
    [Microsoft.AspNetCore.Mvc.Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles="Owner")]
    public class RoutesController : ControllerBase
    {
        private readonly IRouteService _routeService;
        private readonly IMapper _mapper;

        public RoutesController(IRouteService routeService, IMapper mapper)
        {
            _routeService = routeService;
            _mapper = mapper;
        }

        [HttpPost("Create")]
        public ActionResult<RouteViewModel> Create(RouteCreateViewModel model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);


            var route = _routeService.CreateRoute(_mapper.Map<RouteDTO>(model), AccessToken());

            if (route == null) return BadRequest("Something went wrong");

            return _mapper.Map<RouteViewModel>(route);
        }

        [HttpGet]
        public ActionResult<List<RouteViewModel>> Get()
        {
            var routes = _routeService.GetOwnRoutes(AccessToken());

            return _mapper.Map<List<RouteViewModel>>(routes);
        }

        [HttpGet("{id}")]
        public ActionResult<RouteViewModel> Get(int id)
        {
            var route = _routeService.GetRouteById(id);

            return _mapper.Map<RouteViewModel>(route);
        }

        [HttpGet("get-points/{id}")]
        public ActionResult<List<PathPointViewModel>> GetPointsById(int id)
        {
            var points = _routeService.GetPointsByRouteId(id);

            return _mapper.Map<List<PathPointViewModel>>(points);
        }

        [HttpPost("optimize")]
        public ActionResult<List<PathPointViewModel>> GetOptimizedRoute(GetOptimizedRouteViewModel model)
        {
            var points = _routeService.GetOptimizedRoute(model.RouteId, model.StartingPointId);

            return _mapper.Map<List<PathPointViewModel>>(points);
        }

        private JwtSecurityToken AccessToken()
        {
            var requestAccessToken = Request.Cookies["accessToken"];
            return new JwtSecurityTokenHandler().ReadToken(requestAccessToken) as JwtSecurityToken;
        }
    }
}
