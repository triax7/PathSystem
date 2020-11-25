using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using PathSystemServer.DTOs.Routing;

namespace PathSystemServer.Services.Routing
{
    public interface IRouteService
    {
        RouteDTO CreateRoute(RouteDTO dto, JwtSecurityToken token);
        List<RouteDTO> GetOwnRoutes(JwtSecurityToken token);
        RouteDTO GetRouteById(int id);
        List<PathPointDTO> GetPointsByRouteId(int id);
    }
}