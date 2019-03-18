using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Swashbuckle.AspNetCore.Swagger;

namespace PSO_Control_Service {
    public class Startup {
        public Startup (IConfiguration configuration) {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices (IServiceCollection services) {
            services.AddMvc ().SetCompatibilityVersion (CompatibilityVersion.Version_2_2);
            services.AddSwaggerGen (c => {
                c.SwaggerDoc ("v1", new Info { Title = "PSO API", Version = "v1" });
            });
            services.AddDbContext<PSO_Context> (opt =>
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
                Console.WriteLine(connectionString);
                opt.UseNpgsql(connectionString);
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure (IApplicationBuilder app, IHostingEnvironment env) {
            if (env.IsDevelopment ()) {
                app.UseDeveloperExceptionPage ();
            } else {
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts ();
            }

            using (var serviceScope = app.ApplicationServices.GetRequiredService<IServiceScopeFactory>().CreateScope())
            {
                var context = serviceScope.ServiceProvider.GetService<PSO_Context>();
                context.Database.Migrate();
                context.Database.EnsureCreated();
            }

            app.UseSwagger ();
            app.UseSwaggerUI (c => {
                c.SwaggerEndpoint ("/swagger/v1/swagger.json", "PSO API");
            });
            
            app.UseHttpsRedirection ();
            app.UseMvc ();
        }
    }
}