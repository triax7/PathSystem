using System.Collections.Generic;
using PathSystem.DAL.Abstractions;

namespace PathSystem.DAL.Models
{
    public class Route : BaseEntity
    {
        public string Name { get; set; }
        public int OwnerId { get; set; }
        public Owner Owner { get; set; }
        public ICollection<PathPoint> PathPoints { get; set; }
    }
}
