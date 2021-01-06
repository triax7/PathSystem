using System.Linq;
using Microsoft.EntityFrameworkCore;
using PathSystem.DAL.Models;
using PathSystem.DAL.Repositories.Interfaces;

namespace PathSystem.DAL.Repositories.Implementations
{
    public class RouteRepository : BaseRepository<Route>, IRouteRepository
    {
        public RouteRepository(ApplicationContext context)
            : base(context)
        {

        }

        public IQueryable<Route> GetAllWithPoints()
        {
            return _context.Set<Route>().Include(r => r.PathPoints);
        }
    }
}
