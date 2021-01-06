using PathSystem.DAL.Models;

namespace PathSystem.DAL.Repositories.Interfaces
{
    public interface IPathPointRepository : IBaseRepository<PathPoint>
    {
        public PathPoint GetByIdWithRoute(int id);
    }
}
