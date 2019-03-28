using System;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Authentication.Utility
{
    public class StartupDatabaseInitializer
    {
        public static void InitializeDatabase(IServiceCollection services)
        {
            services.AddDbContext<AuthenticationContext>(opt =>
            {
                var host = Environment.GetEnvironmentVariable("POSTGRES_HOST");
                var port = Environment.GetEnvironmentVariable("POSTGRES_PORT");
                var user = Environment.GetEnvironmentVariable("POSTGRES_USER");
                var password = Environment.GetEnvironmentVariable("POSTGRES_PASSWORD");
                var database = Environment.GetEnvironmentVariable("POSTGRES_DB");

                var connectionString = $"Host={host};" +
                                       $"Port={port};" +
                                       $"Username={user};" +
                                       $"Password={password};" +
                                       $"Database={database}";
                opt.UseNpgsql(connectionString);
            });
        }
        
        public static void MigrateDatabase(IApplicationBuilder app)
        {
            using (var serviceScope = app.ApplicationServices.GetRequiredService<IServiceScopeFactory>().CreateScope())
            {
                var context = serviceScope.ServiceProvider.GetService<AuthenticationContext>();
                context.Database.Migrate();
                context.Database.EnsureCreated();
            }
        }
    }
}