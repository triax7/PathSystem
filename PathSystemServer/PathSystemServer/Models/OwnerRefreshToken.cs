using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PathSystemServer.Models
{
    public class OwnerRefreshToken
    {
        public int Id { get; set; }
        public string Token { get; set; }
        public Owner Owner { get; set; }
    }
}
