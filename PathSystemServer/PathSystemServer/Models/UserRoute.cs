using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PathSystemServer.Models
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
