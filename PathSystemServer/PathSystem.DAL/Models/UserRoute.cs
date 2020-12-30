using System;

namespace PathSystem.DAL.Models
{
    public class UserRoute
    {
        public int Id { get; set; }
        public User User { get; set; }
        public Route Route { get; set; }
        public DateTime Started { get; set; }
        public DateTime Finished { get; set; }
    }
}
