using Data.Dapper.Repositories;
using Data.Dapper.Repositories.Interfaces;

namespace API.Infrastructure.Bootstrapping
{
    public static class ConfigureDapperDependencies
    {
        public static IServiceCollection ConfigureDapper(this IServiceCollection services)
        {
            services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
            return services;
        }
    }
}
