using API.Infrastructure.Bootstrapping;
using API.Infrastructure.ExceptionHandling;
using API.Infrastructure.Extensions;

namespace API
{
    [Boilerplate]
    public class Program
    {
        
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
            builder.Services
               .ConfigureDapper()
               .ConfigureFluentMigrator(Environment.GetEnvironmentVariable("DB_CONNECTION"));
            
            var app = builder.Build();
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
            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();
            app.MigrateDatabase();
            app.Run();
        }
    }
}