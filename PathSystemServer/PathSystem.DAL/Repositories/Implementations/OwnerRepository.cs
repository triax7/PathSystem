using System.Linq;
using Microsoft.EntityFrameworkCore;
using PathSystem.DAL.Models;
using PathSystem.DAL.Repositories.Interfaces;

namespace PathSystem.DAL.Repositories.Implementations
{
    public class OwnerRepository : BaseRepository<Owner>, IOwnerRepository
    {
        public OwnerRepository(ApplicationContext context)
            : base(context)
        {

        }

        public IQueryable<Owner> GetAllWithRoutes()
        {
            return _context.Set<Owner>().Include(o => o.Routes);
        }

        public Owner GetByIdWithRoutes(int id)
        {
            return GetAllWithRoutes().SingleOrDefault(o => o.Id == id);
        }
    }
}
