using API.Infrastructure.Bootstrapping;
using API.Infrastructure.ExceptionHandling;
using FluentMigrator.Runner;
using Microsoft.AspNetCore.Mvc;


namespace API.Infrastructure
{

    [Boilerplate]
    public class Startup
    {
        private static readonly List<string> _devEnvironments = new List<string>() { "development", "local" };
        IConfiguration Configuration { get; }

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }



        public void Configure(IApplicationBuilder app, Microsoft.AspNetCore.Hosting.IHostingEnvironment env, IMigrationRunner migrationRunner)
        {
            if (_devEnvironments.Contains(Configuration["ENVIRONMENT"]))
            {
                app
                    .SetupAppForDevelopmentMode()
                    .UseDeveloperExceptionPage();

                if (migrationRunner.HasMigrationsToApplyUp())
                {
                    Console.WriteLine("Running migrations...");
                    migrationRunner.MigrateUp();
                }
            }
            else
                app.UseHsts();

            app.Use(async (context, next) => {
                context.Response.Headers.Add("X-Frame-Options", "SAMEORIGIN");
                context.Response.Headers.Add("Content-Security-Policy", "frame-ancestors https://www.googletagmanager.com/*; https://*.facebook.com/*");
                context.Response.Headers.Add("Strict-Transport-Security", "max-age=31536000; includeSubDomains");
                context.Response.Headers.Add("Cache-Control", "no-store; no-cache");
                await next();
            });

            app
                .UseAuthentication()
                .UseHttpsRedirection()
                .UseCors("CorsPolicy")
                .UseMiddleware<ExceptionHandlerMiddleware>();
               // .UseMvc();

            var isDevelopment = _devEnvironments.Contains(Configuration["ENVIRONMENT"]);
            if (isDevelopment)
                app
                    .UseSwagger()
                    .UseSwaggerUI(c => {
                        c.SwaggerEndpoint("/swagger/v1/swagger.json", "Bookfinity API v1");
                    });
        }
    }
}
