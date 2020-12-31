using System;
using PathSystem.DAL.Abstractions;

namespace PathSystem.DAL.Models
{
    public class UserRoute : BaseEntity
    {
        public User User { get; set; }
        public Route Route { get; set; }
        public DateTime Started { get; set; }
        public DateTime Finished { get; set; }
    }
}
