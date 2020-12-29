using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using PathSystemServer.DTOs.Routing;
using PathSystemServer.Models;
using PathSystemServer.ViewModels.Routing;

namespace PathSystemServer.Services.Routing
{
    public interface IRouteService
    {
        Route CreateRoute(RouteCreateViewModel model, JwtSecurityToken token);
        List<Route> GetOwnRoutes(JwtSecurityToken token);
        Route GetRouteById(int id);
        List<PathPointDTO> GetPointsByRouteId(int id);
        List<PathPointDTO> GetOptimizedRoute(int id, int startingPointId);
    }
}