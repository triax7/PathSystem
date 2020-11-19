using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PathSystemServer.Models
{
    public class UserPathPoint
    {
        public int Id { get; set; }
        public User User { get; set; }
        public PathPoint PathPoint { get; set; }
        public bool Visited { get; set; }
        public DateTime DateVisited { get; set; }
    }
}
