using System;
using PathSystem.DAL.Abstractions;

namespace PathSystem.DAL.Models
{
    public class UserPathPoint : BaseEntity
    {
        public User User { get; set; }
        public PathPoint PathPoint { get; set; }
        public bool Visited { get; set; }
        public DateTime DateVisited { get; set; }
    }
}
