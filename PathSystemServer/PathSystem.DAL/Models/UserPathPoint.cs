using System;

namespace PathSystem.DAL.Models
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
