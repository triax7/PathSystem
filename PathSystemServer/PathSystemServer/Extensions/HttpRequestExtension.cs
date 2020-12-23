using System.IdentityModel.Tokens.Jwt;
using Microsoft.AspNetCore.Http;

namespace PathSystemServer.Extensions
{
    public static class HttpRequestExtension
    {
        public static JwtSecurityToken GetAccessToken(this HttpRequest request)
        {
            var requestAccessToken = request.Cookies["accessToken"];
            return new JwtSecurityTokenHandler().ReadToken(requestAccessToken) as JwtSecurityToken;
        }
    }
}
