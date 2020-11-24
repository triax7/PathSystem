using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PathSystemServer.DTOs.Auth
{
    public class LoginSuccessDTO
    {
        public string Name { get; set; }
        public string AccessToken { get; set; }
        public string RefreshToken { get; set; }

        public LoginSuccessDTO(string name, string accessToken, string refreshToken)
        {
            Name = name;
            AccessToken = accessToken;
            RefreshToken = refreshToken;
        }
    }
}
