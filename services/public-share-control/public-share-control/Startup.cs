using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using PublicShareControl.Database;
using PublicShareControl.Repositories;
using PublicShareControl.Services;
using PublicShareControl.Utility;

namespace PublicShareControl
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            // Set compability mode for mvc
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2)
                .AddJsonOptions(options =>
                    options.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore);

            CorsConfig.AddCorsPolicy(services);
            APIDocumentationInitializer.ApiDocumentationInitializer(services);
            StartupDatabaseInitializer.InitializeDatabase(services);

            services.AddHttpClient();
            
            services.AddScoped<IPortfolioRepository, PortfolioRepository>();
            services.AddScoped<ISharesRepository, SharesRepository>();
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<IUsersService, UsersService>();
            services.AddScoped<ISharesService, SharesService>();
            services.AddScoped<IPortfolioService, PortfolioService>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
                app.UseHttpsRedirection();
            }

            StartupDatabaseInitializer.MigrateDatabase(app);
            APIDocumentationInitializer.AllowAPIDocumentation(app);
            CorsConfig.AddCors(app);
            app.UseMvc();
        }
    }
}