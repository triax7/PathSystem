using System.Collections.Generic;
using NetTopologySuite.Geometries;
using PathSystem.DAL.Abstractions;

namespace PathSystem.DAL.Models
{
    public class PathPoint : BaseEntity
    {
        public string Name { get; set; }
        public Point Point { get; set; }
        public Route Route { get; set; }
        public ICollection<UserPathPoint> UserPathPoints { get; set; }
    }
}
