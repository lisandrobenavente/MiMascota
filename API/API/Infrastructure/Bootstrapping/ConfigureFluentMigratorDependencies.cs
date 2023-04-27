using Microsoft.Extensions.DependencyInjection;
using FluentMigrator.Runner;
using Data.Dapper.Migrations;

namespace API.Infrastructure.Bootstrapping
{
    public static class ConfigureFluentMigratorDependencies
    {
        public static IServiceCollection ConfigureFluentMigrator(this IServiceCollection services, string connectionString)
        {
            services.AddFluentMigratorCore()
                .ConfigureRunner(r => r
                    .AddMySql5()
                    .WithGlobalConnectionString(connectionString)
                    .ScanIn(typeof(AddUsersTable).Assembly).For.Migrations())
                .AddLogging(fl => fl.AddFluentMigratorConsole());
            return services;
        }
    }
}
