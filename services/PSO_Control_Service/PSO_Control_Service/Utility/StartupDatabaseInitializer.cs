using System;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace PSO_Control_Service.Utility
{
    public class StartupDatabaseInitializer
    {
        public static void InitializeDatabase(IServiceCollection services)
        {
            services.AddDbContext<PSO_Context>(opt =>
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
                var context = serviceScope.ServiceProvider.GetService<PSO_Context>();
                context.Database.Migrate();
                context.Database.EnsureCreated();
            }
        }
    }
}