using Microsoft.Extensions.DependencyInjection;
using PathSystem.DAL.Repositories;
using PathSystem.DAL.Repositories.Implementations;
using PathSystem.DAL.Repositories.Interfaces;

namespace PathSystem.DAL
{
    public static class RegisterServices
    {
        public static IServiceCollection AddRepositories(this IServiceCollection services)
        {
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IUserRefreshTokenRepository, UserRefreshTokenRepository>();
            services.AddScoped<IOwnerRepository, OwnerRepository>();
            services.AddScoped<IOwnerRefreshTokenRepository, OwnerRefreshTokenRepository>();
            services.AddScoped<IRouteRepository, RouteRepository>();
            services.AddScoped<IPathPointRepository, PathPointRepository>();

            services.AddScoped<IUnitOfWork, UnitOfWork>();

            return services;
        }
    }
}
