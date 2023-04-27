using System;
using Microsoft.Extensions.DependencyInjection;
using API.Infrastructure.ExceptionHandling.Renderers;
using Data.Dapper.Repositories.Interfaces;
using Data.Dapper.Repositories;

namespace API.Infrastructure.Bootstrapping
{
    public static class DependencyBootstrapper
    {
        public static IServiceCollection ConfigureDataDependencies(this IServiceCollection services)
        {
           services.AddScoped<IUsersRepository, UsersRepository>();
            return services;
        }

        public static IServiceCollection ConfigureServiceDependencies(this IServiceCollection services)
        {
            return services;
        }

        public static IServiceCollection ConfigureWebDependencies(this IServiceCollection services)
        {
            services.Scan(scan =>
                scan.FromAssemblyOf<IExceptionRender>()
                    .AddClasses(c => c.AssignableTo<IExceptionRender>())
                    .AsImplementedInterfaces().WithSingletonLifetime()
            );
            return services;
        }

        public static IServiceCollection ConfigureAuxiliaryServices(this IServiceCollection services)
        {
            
           // services.AddSingleton<IApiInformationGetter, DefaultApiInformationGetter>();
            return services;
        }
    }
}
