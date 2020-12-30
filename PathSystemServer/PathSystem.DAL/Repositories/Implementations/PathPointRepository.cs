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
    }
}
