using System.IdentityModel.Tokens.Jwt;
using PathSystemServer.DTOs.Routing;

namespace PathSystemServer.Services.Routing
{
    public interface IPathPointService
    {
        PathPointDTO AddPointToRoute(int routeId, PathPointDTO dto, JwtSecurityToken token);
    }
}