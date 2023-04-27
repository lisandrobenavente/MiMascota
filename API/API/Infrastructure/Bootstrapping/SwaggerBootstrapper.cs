using Microsoft.OpenApi.Models;

namespace API.Infrastructure.Bootstrapping
{
    public static class SwaggerBootstrapper
    {
        public static IServiceCollection ConfigureSwagger(this IServiceCollection services)
        {
            services.AddSwaggerGen(c => {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Mi Mascota API", Version = "v1" });
            });
            return services;
        }

        public static IApplicationBuilder SetupAppForDevelopmentMode(this IApplicationBuilder app)
        {
            app.UseSwagger();

            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint(SwaggerConfiguration.EndpointUrl, SwaggerConfiguration.EndpointDescription);
            });

            return app;
        }
    }
}
