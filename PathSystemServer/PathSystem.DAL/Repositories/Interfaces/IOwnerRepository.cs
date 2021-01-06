using System.Linq;
using PathSystem.DAL.Models;

namespace PathSystem.DAL.Repositories.Interfaces
{
    public interface IOwnerRepository : IBaseRepository<Owner>
    {
        public IQueryable<Owner> GetAllWithRoutes();
        public Owner GetByIdWithRoutes(int id);
    }
}
