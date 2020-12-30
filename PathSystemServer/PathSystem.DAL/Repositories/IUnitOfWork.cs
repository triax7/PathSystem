using PathSystem.DAL.Repositories.Interfaces;

namespace PathSystem.DAL.Repositories
{
    public interface IUnitOfWork
    {
        IUserRepository Users { get; }
        IUserRefreshTokenRepository UserRefreshTokens { get; }
        IOwnerRepository Owners { get; }
        IOwnerRefreshTokenRepository OwnerRefreshTokens { get; }
        IRouteRepository Routes { get; }
        IPathPointRepository PathPoints { get; }
        int Commit();
    }
}