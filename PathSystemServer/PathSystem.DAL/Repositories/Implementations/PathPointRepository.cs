using System.Linq;
using Microsoft.EntityFrameworkCore;
using PathSystem.DAL.Models;
using PathSystem.DAL.Repositories.Interfaces;

namespace PathSystem.DAL.Repositories.Implementations
{
    public class PathPointRepository : BaseRepository<PathPoint>, IPathPointRepository
    {
        public PathPointRepository(ApplicationContext context)
            : base(context)
        {

        }

        public PathPoint GetByIdWithRoute(int id)
        {
            return _context.Set<PathPoint>().Include(p => p.Route).SingleOrDefault(p => p.Id == id);
        }
    }
}
