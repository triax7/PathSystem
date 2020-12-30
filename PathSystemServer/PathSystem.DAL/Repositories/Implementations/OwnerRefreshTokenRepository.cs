using PathSystem.DAL.Models;
using PathSystem.DAL.Repositories.Interfaces;

namespace PathSystem.DAL.Repositories.Implementations
{
    public class OwnerRefreshTokenRepository : BaseRepository<OwnerRefreshToken>, IOwnerRefreshTokenRepository
    {
        public OwnerRefreshTokenRepository(ApplicationContext context)
            : base(context)
        {

        }
    }
}
