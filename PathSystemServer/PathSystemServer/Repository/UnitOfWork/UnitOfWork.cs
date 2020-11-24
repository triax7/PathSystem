using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PathSystemServer.Repository.Interfaces;

namespace PathSystemServer.Repository.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationContext _context;
        public IUserRepository Users { get; }
        public IRefreshTokenRepository RefreshTokens { get; }
        public IOwnerRepository Owners { get; }
        public IOwnerRefreshTokenRepository OwnerRefreshTokens { get; }
        public IRouteRepository Routes { get; }

        public UnitOfWork(ApplicationContext context, IUserRepository userRepository,
            IRefreshTokenRepository refreshTokenRepository, IOwnerRepository ownerRepository,
            IOwnerRefreshTokenRepository ownerRefreshTokenRepository, IRouteRepository routeRepository)
        {
            _context = context;
            Users = userRepository;
            RefreshTokens = refreshTokenRepository;
            Owners = ownerRepository;
            OwnerRefreshTokens = ownerRefreshTokenRepository;
            Routes = routeRepository;
        }

        public int Commit()
        {
            return _context.SaveChanges();
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}