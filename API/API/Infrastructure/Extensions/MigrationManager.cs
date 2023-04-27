using FluentMigrator.Runner;

namespace API.Infrastructure.Extensions
{
    [Boilerplate]
    public static class MigrationManager
    {
        public static IHost MigrateDatabase(this IHost host)
        {
            
            using (var scope = host.Services.CreateScope())
            {
                var migrationRunner = scope.ServiceProvider.GetRequiredService<IMigrationRunner>();
                if (migrationRunner.HasMigrationsToApplyUp())
                    {
                        Console.WriteLine("Running migrations...");
                        migrationRunner.MigrateUp();
                    }
                
            }
            return host;
        }
    }
}
