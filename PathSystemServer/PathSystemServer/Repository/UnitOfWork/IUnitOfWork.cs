using PathSystemServer.Repository.Interfaces;

namespace PathSystemServer.Repository.UnitOfWork
{
    public interface IUnitOfWork
    {
        IUserRepository Users { get; }
        IRefreshTokenRepository RefreshTokens { get; }
        IOwnerRepository Owners { get; }
        int Commit();
        void Dispose();
    }
}