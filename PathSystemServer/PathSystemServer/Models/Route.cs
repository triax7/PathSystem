using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using NetTopologySuite.Geometries;

namespace PathSystemServer.Models
{
    public class Route
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public ICollection<PathPoint> PathPoints { get; set; }
    }
}
