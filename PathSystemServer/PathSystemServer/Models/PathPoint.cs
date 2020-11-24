using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using NetTopologySuite.Geometries;

namespace PathSystemServer.Models
{
    public class PathPoint
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public Point Point { get; set; }
        public Route Route { get; set; }
        public ICollection<UserPathPoint> UserPathPoints { get; set; }
    }
}
