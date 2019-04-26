using System;
using Authentication.Database;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Authentication.Utility {
    public class StartupDatabaseInitializer {
        public static void InitializeDatabase (IServiceCollection services) {
            var env = Environment.GetEnvironmentVariable ("RUNTIME_ENV");

            switch (env) {
                case "PRODUCTION":
                    services.AddDbContext<AuthenticationContext> (opt => {
                        var host = Environment.GetEnvironmentVariable ("POSTGRES_HOST");
                        var port = Environment.GetEnvironmentVariable ("POSTGRES_PORT");
                        var user = Environment.GetEnvironmentVariable ("POSTGRES_USER");
                        var password = Environment.GetEnvironmentVariable ("POSTGRES_PASSWORD");
                        var database = Environment.GetEnvironmentVariable ("POSTGRES_DB");

                        var connectionString = $"Host={host};" +
                            $"Port={port};" +
                            $"Username={user};" +
                            $"Password={password};" +
                            $"Database={database}";
                        opt.UseNpgsql (connectionString);
                    });
                    break;
                default:
                case "TEST":
                    services.AddDbContext<AuthenticationContext> (ops => { ops.UseInMemoryDatabase (databaseName: "Authentication"); });
                    break;
            }
        }

        public static void MigrateDatabase (IApplicationBuilder app) {
            var env = Environment.GetEnvironmentVariable ("RUNTIME_ENV");

            switch (env) {
                case "PRODUCTION":
                    using (var serviceScope = app.ApplicationServices.GetRequiredService<IServiceScopeFactory> ()
                        .CreateScope ()) {
                        var context = serviceScope.ServiceProvider.GetService<AuthenticationContext> ();
                        context.Database.Migrate ();
                        context.Database.EnsureCreated ();
                    }

                    break;
                default:
                case "TEST":
                    break;
            }
        }
    }
}