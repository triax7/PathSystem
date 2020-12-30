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
    }
}
