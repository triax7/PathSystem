using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PathSystemServer.ViewModels.Routing
{
    public class PathPointViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public double Longitude { get; set; }
        public double Latitude { get; set; }
    }
}
