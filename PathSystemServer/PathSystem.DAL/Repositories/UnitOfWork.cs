using PathSystem.DAL.Repositories.Interfaces;

namespace PathSystem.DAL.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationContext _context;
        public IUserRepository Users { get; }
        public IUserRefreshTokenRepository UserRefreshTokens { get; }
        public IOwnerRepository Owners { get; }
        public IOwnerRefreshTokenRepository OwnerRefreshTokens { get; }
        public IRouteRepository Routes { get; }
        public IPathPointRepository PathPoints { get; }

        public UnitOfWork(ApplicationContext context, IUserRepository userRepository,
            IUserRefreshTokenRepository userRefreshTokenRepository, IOwnerRepository ownerRepository,
            IOwnerRefreshTokenRepository ownerRefreshTokenRepository, IRouteRepository routeRepository,
            IPathPointRepository pathPointRepository)
        {
            _context = context;
            Users = userRepository;
            UserRefreshTokens = userRefreshTokenRepository;
            Owners = ownerRepository;
            OwnerRefreshTokens = ownerRefreshTokenRepository;
            Routes = routeRepository;
            PathPoints = pathPointRepository;
        }

        public int Commit()
        {
            return _context.SaveChanges();
        }
    }
}