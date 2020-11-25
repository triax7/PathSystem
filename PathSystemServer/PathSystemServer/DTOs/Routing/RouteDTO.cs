using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PathSystemServer.DTOs.Routing
{
    public class RouteDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        private List<PathPointDTO> PathPoints { get; set; }
    }
}
