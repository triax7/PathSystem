using PathSystemServer.Repository.Interfaces;

namespace PathSystemServer.Repository.UnitOfWork
{
    public interface IUnitOfWork
    {
        IUserRepository Users { get; }
        IRefreshTokenRepository RefreshTokens { get; }
        IOwnerRepository Owners { get; }
        IOwnerRefreshTokenRepository OwnerRefreshTokens { get; }
        IRouteRepository Routes { get; }
        IPathPointRepository PathPoints { get; }
        int Commit();
        void Dispose();
    }
}