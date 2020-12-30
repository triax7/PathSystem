using System.Collections.Generic;

namespace PathSystem.DAL.Models
{
    public class Route
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int OwnerId { get; set; }
        public Owner Owner { get; set; }
        public ICollection<PathPoint> PathPoints { get; set; }
    }
}
