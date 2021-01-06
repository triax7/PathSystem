using System.Linq;
using PathSystem.DAL.Models;

namespace PathSystem.DAL.Repositories.Interfaces
{
    public interface IRouteRepository : IBaseRepository<Route>
    {
        public IQueryable<Route> GetAllWithPoints();
    }
}
