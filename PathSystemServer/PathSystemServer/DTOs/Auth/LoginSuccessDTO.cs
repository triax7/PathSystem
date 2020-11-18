using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PathSystemServer.DTOs.Auth
{
    public class LoginSuccessDTO
    {
        public string UserName { get; set; }
        public string AccessToken { get; set; }
        public string RefreshToken { get; set; }

        public LoginSuccessDTO(string userName, string accessToken, string refreshToken)
        {
            UserName = userName;
            AccessToken = accessToken;
            RefreshToken = refreshToken;
        }
    }
}
